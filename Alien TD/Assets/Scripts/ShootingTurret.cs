using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingTurret : MonoBehaviour {

    [SerializeField] public float Range = 15;
    [Range(0,1)][SerializeField] public float RateOfFire;
    [SerializeField] public float Damage = 20;
    [SerializeField] protected GameObject  Target;
    //[SerializeField] public float ExplosionRadius = 2f;
    //public static ShootingTurret Instance;

    [SerializeField] public List<GameObject> EnemyList = new List<GameObject> () ;

    public abstract void SearchForTarget();
    public abstract void Shoot();

}
