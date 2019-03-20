using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bulletv2 : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 20f)]
    public float speed;
    [SerializeField]
    public GameObject impactEffect;
    [SerializeField]
    protected Transform target;
    [SerializeField]
    [Range(0, 1f)]
    public float effectTime;
    public float damage;

    public void Initialize(Transform _target, float _damage)
    {
        target = _target;
        damage = _damage;
    }

    public abstract void HitTarget();

    protected void ImpactEffect()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, effectTime);
    }

    public void BulletBehaviour()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);


    }

    public void targetDamage(Transform enemy)
    {
        enemy.GetComponent<BasicEnemyScript>().LoseHealth(damage);
        
    }
}
