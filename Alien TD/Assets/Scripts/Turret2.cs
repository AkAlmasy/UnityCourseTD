using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret2 : MonoBehaviour
{

    protected Transform target;
    protected BasicEnemyScript targetEnemy;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    protected float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    //Fordulási sebesség
    public float turnSpeed = 10f;

    //public GameObject bulletPrefab;
    //Bullet Spawner point
    public Transform firePoint;

    void Awake()
    {
        targetEnemy = GetComponent<BasicEnemyScript>();
        //target = GetComponent<Transform>();
        //ne ismételje meg minden egyes Update-ban ezt a metódust, elég kevesebbszer is (gépnek jót tesz)
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected void UpdateTarget()
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
        }
        else
        {
            target = null;
        }

    }
    protected void LockOnTarget()
    {
        //ez a vector fogja tárolni azt az irányt ami az ellenségre fog mutatni
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);//Quaternion -> Unity-ban kezeli a rotationt, nekünk csak tudni kell hogyan nézzünk arra
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Csak az y tengely mentén forogjon
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected bool canIShoot()
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
                //shortestDistance = distanceToEnemy;
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (shortestDistance > range)
            return false;
        else
            return true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected abstract void Shoot();
}
