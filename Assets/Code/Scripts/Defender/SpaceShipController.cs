using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] private BulletFactory bulletFactory;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float speed;
    private bool isAllowToFire = true;
    private Vector3 originalPosition = new Vector3(0f, 0.25f, 0f);
    private BulletBase.BulletTypes currentBulletType;

    private void Start()
    {
        EventDispatcher.Subscribe<BulletBase>(OnReleaseBullet);
        rigidbody2D.gravityScale = 0;
    }

    private void OnEnable()
    {
        transform.localPosition = originalPosition;
        currentBulletType = BulletBase.BulletTypes.Fast;
    }

    private void OnReleaseBullet(BulletBase bullet)
    {
        bulletFactory.Release(bullet);
        isAllowToFire = true;
    }

    void Update()
    {
        HandleMovement();
        HandleShootBullet();
        HandleChangeBulletType();
    }

    private void HandleChangeBulletType()
    {
        var bulletTypeAsInt = (int)currentBulletType;
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            bulletTypeAsInt += 1;
            bulletTypeAsInt %= Enum.GetValues(typeof(BulletBase.BulletTypes)).Length;
            currentBulletType = (BulletBase.BulletTypes)bulletTypeAsInt;
            EventDispatcher.Publish(new BulletTypeChangeEvent()
            {
                bulletType = currentBulletType
            });
        }
    }

    private void HandleShootBullet()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isAllowToFire)
        {
            bulletFactory.Fire(bulletPosition.position, currentBulletType);
            isAllowToFire = false;
        }
    }

    private void HandleMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalInput * speed, 0);
    }

    private void OnDestroy()
    {
        EventDispatcher.Unsubscribe<BulletBase>(OnReleaseBullet);
    }
}
