using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraight : BulletBase
{
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime);
    }
}
