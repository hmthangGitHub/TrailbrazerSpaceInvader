using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class BulletFactory : MonoBehaviour
{
    public BulletBase[] bulletPrefabs;
    private readonly Dictionary<BulletBase.BulletTypes, BulletBase> bulletInstances = new();

    private void Start()
    {
        foreach (var bullet in bulletPrefabs)
        {
            var bulletInstance = Instantiate(bullet);
            bulletInstance.gameObject.SetActive(false);
            bulletInstances.Add(bullet.BulletType, bulletInstance);
        }
    }

    public void Fire(Vector3 position, BulletBase.BulletTypes bulletType)
    {
        var bulletInstance = bulletInstances[bulletType];
        bulletInstance.transform.position = position;
        bulletInstance.gameObject.SetActive(true);
    }

    public void Release(BulletBase bulletBase)
    {
        bulletBase.gameObject.SetActive(false);
    }
}
