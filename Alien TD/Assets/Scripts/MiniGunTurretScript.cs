using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGunTurretScript : ShootingTurret
{

    [SerializeField] public Light FireLight;
    [SerializeField] [Range(0, 100)] public int ChanceOfHit;
    [SerializeField] [Range(0, .25f)] public float ROFOffset = .1f;

    float LastShot;

    private void Update()
    {
        if (Target == null)
        {
            EnemyList = GameManager.Instance.EnemiesAlive;
            SearchForTarget();
        }
        else Shoot();
    }


    public override void SearchForTarget()
    {

        foreach (GameObject t in EnemyList)
        {


            if (EnemyList.Contains(t))
            {
                float Distance = Vector3.Distance(transform.position, t.transform.position);

                if (Distance <= Range && t != null)
                {
                    Target = t;
                    break;
                }
            }
            if (!GameManager.Instance.EnemiesAlive.Contains(t))
                Target = null;
        }
    }

    public override void Shoot()
    {
        if (EnemyList.Contains(Target))
            StartCoroutine("LockOnAndShoot");
        else
            Target = null;


    }

    IEnumerator LockOnAndShoot()
    {
        transform.LookAt(Target.transform);
        yield return new WaitForSeconds(RateOfFire - ROFOffset);
        FireLight.enabled = true;
        yield return new WaitForSeconds(ROFOffset);
        FireLight.enabled = false;
        DamageEnemy();

        CheckForDistance();

    }


    void CheckForDistance()
    {
        float Distance = Vector3.Distance(transform.position, Target.transform.position);

        if (Distance > Range || Target.tag != "Enemy")
        {
            Target = null;
            // StopCoroutine("LockOnAndShoot");
        }
    }
    void DamageEnemy()
    {
        int RND = Random.Range(0, 100);
        if (RND < ChanceOfHit)
            Target.GetComponent<BasicEnemyScript>().LoseHealth(Damage);

    }
}
