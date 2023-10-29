using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadersManager : MonoBehaviour
{
    [SerializeField] private SimpleGameObjectGridLayout simpleGameObjectGridLayout;
    [SerializeField] private InvaderFactory invaderFactory;
    [SerializeField] private int numberInvaderInRow;
    [SerializeField] private Vector2Int rangeNumberOfRow;

    private int currentNumberOfInvaderDead;
    private int currentNumberOfInvader;
    
    private void OnEnable()
    {
        var numberOfRow = UnityEngine.Random.Range(rangeNumberOfRow.x, rangeNumberOfRow.y);
        invaderFactory.CreateInvaders(numberInvaderInRow, numberOfRow);
        simpleGameObjectGridLayout.CalculateGridLayout();
        
        currentNumberOfInvader = numberOfRow * numberInvaderInRow; 
        currentNumberOfInvaderDead = 0;
        EventDispatcher.Subscribe<InvaderDeadEvent>(OnInvaderDeadEvent);
    }

    private void OnInvaderDeadEvent(InvaderDeadEvent invaderDeadEvent)
    {
        currentNumberOfInvaderDead++;
        if (currentNumberOfInvaderDead == currentNumberOfInvader)
        {
            EventDispatcher.Publish(new ChangeGameStateEvent()
            {
                nextState = GameManager.State.Win
            });
        }
    }

    private void OnDisable()
    {
        EventDispatcher.Unsubscribe<InvaderDeadEvent>(OnInvaderDeadEvent);
    }
}
