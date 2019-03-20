using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private BasicEnemyScript targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    //másodpercenkénti sebzés
    public int damageOverTime = 30;
    public float slowAmount= .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    //Fordulási sebesség
    public float turnSpeed = 10f;

    //public GameObject bulletPrefab;
    //Bullet Spawner point
    public Transform firePoint;

    void Start () {
        targetEnemy = GetComponent<BasicEnemyScript>();
        //target = GetComponent<Transform>();
        //ne ismételje meg minden egyes Update-ban ezt a metódust, elég kevesebbszer is (gépnek jót tesz)
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
    //Megtalálja a hozzá legközelebbi célpontot
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Tárolja a legközelebbi ellenséget a közelben
        //Ha nem talál ellenséget
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //Ha találunk ellenséget akkor
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //Megtaláljuk a legközelebbi célpontot a taget lesz a legközelebbi célpont
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<BasicEnemyScript>();
        }else
        {
            target = null;
        }

    }
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                    
            }

            return;
        }
            

        LockOnTarget();
        //Ha használ Lasert
        if (useLaser)
        {
            Laser();
        }
        else ////Különben az alaplövést használja
        {
            //Lövés
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

        
	}

    void LockOnTarget()
    {
        //ez a vector fogja tárolni azt az irányt ami az ellenségre fog mutatni
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);//Quaternion -> Unity-ban kezeli a rotationt, nekünk csak tudni kell hogyan nézzünk arra
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Csak az y tengely mentén forogjon
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
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

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
