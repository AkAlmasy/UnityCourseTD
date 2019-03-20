using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLauncher : Turret2 {

    void Update()
    {
        try
        {
            LockOnTarget();
        }
        catch (NullReferenceException e)
        {

        }

        if (fireCountdown <= 0f)
        {

            if (canIShoot() != false)
            {
                Shoot();
            }

            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    protected override void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }
}
