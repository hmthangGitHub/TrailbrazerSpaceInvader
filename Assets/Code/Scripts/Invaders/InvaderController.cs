using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvaderController : MonoBehaviour
{
    public enum Type
    {
        Red,
        Green,
        Blue
    }
    
    public enum Direction
    {
        Left,
        Right
    }
    
    [SerializeField] private InvaderMovementData invaderMovementData;
    [field : SerializeField] public Type InvaderType { get; private set; }
    [SerializeField] private int HP = 1;
    [SerializeField] private int score = 10;

    private int currentHp;
    private int currentFrame;
    private Direction currentDirection;
    private float currentSpeed;

    private void OnEnable()
    {
        currentSpeed = invaderMovementData.OriginalSpeed;
        currentHp = HP;
        currentFrame = 0;
        currentDirection = Direction.Right;
        EventDispatcher.Subscribe<DirectionChangeEvent>(ChangeDirectionAndSpeed);
    }

    private void OnDisable()
    {
        EventDispatcher.Unsubscribe<DirectionChangeEvent>(ChangeDirectionAndSpeed);
    }

    private void FixedUpdate()
    {
        currentFrame++;
        if (currentFrame == invaderMovementData.NumberOfFramesToUpdatePosition)
        {
            currentFrame = 0;
            transform.position += new Vector3(currentSpeed * Time.fixedDeltaTime * invaderMovementData.NumberOfFramesToUpdatePosition, 0);
        }
    }

    private void ChangeDirectionAndSpeed(DirectionChangeEvent directionChangeEvent)
    {
        if (currentDirection == directionChangeEvent.direction) return;
        currentDirection = directionChangeEvent.direction;
        currentSpeed =  Mathf.Abs(currentSpeed * invaderMovementData.SpeedChangeModifier) * (currentDirection == InvaderController.Direction.Left ? -1.0f : 1.0f);
        transform.position -= new Vector3(0, invaderMovementData.YPositionChange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleDirectionChange(other);
        HandleGetShoot(other);
        HandleIfReachBottom(other);
    }

    private void HandleIfReachBottom(Collider2D other)
    {
        if (other.CompareTag(PhysicTagDefine.BottomTag))
        {
            ChangeGameStateEvent changeGameStateEvent = new()
            {
                nextState = GameManager.State.GameOver
            };
            EventDispatcher.Publish(changeGameStateEvent);
        }
    }

    private void HandleGetShoot(Collider2D other)
    {
        if (other.CompareTag(PhysicTagDefine.BulletTag))
        {
            currentHp -= other.GetComponent<IBullet>()
                              .Damage;
            if (currentHp <= 0)
            {
                var invaderDeadEvent = new InvaderDeadEvent
                {
                    score = score,
                    invaderController = this
                };
                EventDispatcher.Publish(invaderDeadEvent);
            }
        }
    }

    private static void HandleDirectionChange(Collider2D other)
    {
        if (other.CompareTag(PhysicTagDefine.LeftTag))
        {
            var directionChange = new DirectionChangeEvent
            {
                direction = Direction.Right
            };
            EventDispatcher.Publish(directionChange);
        }

        if (other.CompareTag(PhysicTagDefine.RightTag))
        {
            var directionChange = new DirectionChangeEvent
            {
                direction = Direction.Left
            };
            EventDispatcher.Publish(directionChange);
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Unsubscribe<DirectionChangeEvent>(ChangeDirectionAndSpeed);
    }
}