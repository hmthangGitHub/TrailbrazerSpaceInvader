using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    BulletBase.BulletTypes BulletType { get; }
    int Damage { get; }
}

public class BulletBase : MonoBehaviour, IBullet
{
    public enum BulletTypes
    {
        Fast = 0,
        Heavy = 1,
        Zigzag = 2
    }

    [SerializeField] protected float speed;

    [field : SerializeField] public int Damage { get; private set; }
    [field : SerializeField] public BulletTypes BulletType { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag(PhysicTagDefine.TopTag) && !collider.CompareTag(PhysicTagDefine.InvaderTag)) return;
        EventDispatcher.Publish(this);
    }
}