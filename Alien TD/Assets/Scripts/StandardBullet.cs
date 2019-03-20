using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : Bulletv2
{
    public override void HitTarget()
    {
        ImpactEffect();
        targetDamage(target);
        Destroy(gameObject);
    }


    void Update()
    {
        BulletBehaviour();
    }
}
