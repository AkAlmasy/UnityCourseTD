using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bulletv2
{
    [SerializeField] [Range(1f,5f)] public float explosionRadius;
    //public ExplosionBullet expl;

    void Update()
    {
        BulletBehaviour();
    }

    public override void HitTarget()
    {
        ImpactEffect();
        Explode();
        Destroy(gameObject);
    }
    

    void Explode()
    {
        //GameObject explo = (GameObject)Instantiate();
        try
        {
            //RocketTurret rt = (RocketTurret)Instantiate(RocketTurret.Instance);
            //RocketTurret rocketTurret = this.GetComponent<RocketTurret>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    targetDamage(collider.transform);
                }
            }
        }
        catch (NullReferenceException e)
        {

        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
