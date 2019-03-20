using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamer : Turret2
{

    [Header("Use Laser")]
    //public bool useLaser = false;
    //másodpercenkénti sebzés
    public int damageOverTime = 15;
    public float slowAmount = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    void Update()
    {
        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }
        }
        try
        {
            LockOnTarget();
            Shoot();
        }
        catch (NullReferenceException e)
        {

        }
        
    }

    protected override void Shoot()
    {
        //Damage
        //target.GetComponent<NormalAlien>().LoseHealth(damageOverTime * Time.deltaTime); //Enemy?
        targetEnemy.LoseHealth(damageOverTime * Time.deltaTime);
        //target.GetComponent<AntAlien>().LoseHealth(damageOverTime * Time.deltaTime);
        //Speed
        targetEnemy.Slow(slowAmount);

        //Grafika
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play(); //ImpactEffect lejátszása
            impactLight.enabled = true;
        }


        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        //
        Vector3 dir = firePoint.position - target.position;

        //A lézer effectje (ImpactEffect) kövesse az elleneséget/ dir.normalized -> a vektor hossza 1 legyen
        impactEffect.transform.position = target.position + dir.normalized;

        //Az animációból kilépő kis effectek (jelen esetben kockák) a lézer irányába hulljanak
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
}
