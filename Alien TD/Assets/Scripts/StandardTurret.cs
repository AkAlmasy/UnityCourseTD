using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTurret : Turret2
{

    // Use this for initialization
    /*void Start()
    {
        targetEnemy = GetComponent<AntAlien>();
        //target = GetComponent<Transform>();
        //ne ismételje meg minden egyes Update-ban ezt a metódust, elég kevesebbszer is (gépnek jót tesz)
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }*/

    // Update is called once per frame
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
