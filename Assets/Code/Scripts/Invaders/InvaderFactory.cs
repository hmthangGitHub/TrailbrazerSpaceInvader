using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

public class InvaderFactory : MonoBehaviour
{
    [SerializeField] private InvaderController[] invaderPrefabs;
    private Dictionary<InvaderController.Type, ObjectPool<InvaderController>> invaderPools;
    private Dictionary<InvaderController.Type, ObjectPool<InvaderController>> InvaderPools => invaderPools ??= CreateInstancePools();

    private void Start()
    {
        EventDispatcher.Subscribe<InvaderDeadEvent>(OnInvaderDeadEvent);
    }

    private void OnInvaderDeadEvent(InvaderDeadEvent invaderDeadEvent)
    {
        InvaderPools[invaderDeadEvent.invaderController.InvaderType].Release(invaderDeadEvent.invaderController);
    }

    private Dictionary<InvaderController.Type, ObjectPool<InvaderController>> CreateInstancePools()
    {
        var instancePools = new Dictionary<InvaderController.Type, ObjectPool<InvaderController>>();
        foreach (var invaderPrefab in invaderPrefabs)
        {
            instancePools.Add(invaderPrefab.InvaderType, new ObjectPool<InvaderController>(
                () => OnCreateInvader(invaderPrefab),
                OnGetInvader,
                OnReleaseInvader,
                OnDestroyInvader));
        }

        return instancePools;
    }

    private void OnDestroyInvader(InvaderController instance)
    {
        Destroy(instance.gameObject);
    }

    private void OnReleaseInvader(InvaderController instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = default;
    }

    private void OnGetInvader(InvaderController instance)
    {
        instance.gameObject.SetActive(true);
        instance.transform.parent = transform;
    }

    private InvaderController OnCreateInvader(InvaderController invaderPrefab)
    {
        return Instantiate(invaderPrefab);
    }

    public void CreateInvaders(int numberOfInvaderInRow, int numberOfRow)
    {
        ReleaseOldInvaders();
        CreateNewInvaders(numberOfInvaderInRow, numberOfRow);
    }

    private void CreateNewInvaders(int numberOfInvaderInRow,
                                   int numberOfRow)
    {
        for (int i = 0; i < numberOfRow; i++)
        {
            var randomInvaderPool = InvaderPools.ElementAt(UnityEngine.Random.Range(0, InvaderPools.Count)).Value;
            for (int j = 0; j < numberOfInvaderInRow; j++)
            {
                randomInvaderPool.Get();
            }
        }
    }

    private void ReleaseOldInvaders()
    {
        while (transform.childCount != 0)
        {
            var invader = transform.GetChild(0).GetComponent<InvaderController>();
            InvaderPools[invader.InvaderType].Release(invader);
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Unsubscribe<InvaderDeadEvent>(OnInvaderDeadEvent);
    }
}
