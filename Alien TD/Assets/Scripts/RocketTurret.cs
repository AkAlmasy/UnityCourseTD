using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : ShootingTurret {

    [Header("Missle Tower")]
    [SerializeField]
    [Range(0, 1f)]
    public float ROFOffset = .1f;
    [SerializeField]
    public GameObject bulletPrefab;
    public static float explosionRadius = 2f;
    [SerializeField]
    public Transform firePoint;

    void Update()
    {
        Shoot();
    }


    public override void SearchForTarget()
    {
        EnemyList = GameManager.Instance.EnemiesAlive;
        foreach (GameObject t in GameManager.Instance.EnemiesAlive)
        {
            float Distance = Vector3.Distance(transform.position, t.transform.position);
            Debug.Log(Distance);
            if (Distance <= Range)
                Target = t;
        }
    }

    public override void Shoot()
    {

        if (Target != null)
            if (Vector3.Distance(transform.position, Target.transform.position) > Range)
                Target = null;
        if (Target == null) SearchForTarget();
        if (Target != null)
        {


            //transform.LookAt(Target.transform);
            transform.LookAt(Target.transform);
            if (RateOfFire <= 0f)
            {

                DamageEnemy();

                CheckForDistance();

                RateOfFire = 1f / ROFOffset;
            }
        }
        RateOfFire -= Time.deltaTime;

    }
    void CheckForDistance()
    {
        float Distance = Vector3.Distance(transform.position, Target.transform.position);

        if (Distance > Range || Target.tag != "Enemy")
        {
            Target = null;
            StopCoroutine("LockOnAndShoot");
        }
    }
    void DamageEnemy()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        ExplosionBullet explosionBullet = bulletGO.GetComponent<ExplosionBullet>();

        if (explosionBullet != null)
            explosionBullet.Initialize(Target.transform, Damage);
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }*/
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(firePoint.position, Range);
    }
}
