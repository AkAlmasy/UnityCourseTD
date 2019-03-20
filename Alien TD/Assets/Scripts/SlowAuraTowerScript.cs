using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAuraTowerScript : AuraTower  {

    [SerializeField] public Resist Slow;
    [SerializeField] [Range(0, 1)] public float SlowPercentage;
    private void Update()
    {
        
      
        InRange();
    }

    public override void InRange()
    {
        foreach (GameObject t in GameManager.Instance.EnemiesAlive)
        {


            float Distance = Vector3.Distance(transform.position, t.transform.position);
            Debug.Log(Distance);
            if (Distance <= Range && !ListOfEnemiesInAura.Contains(t))
            {
                ListOfEnemiesInAura.Add(t);
                ApplyAura(t);
            }
            if (Distance > Range&& ListOfEnemiesInAura .Contains (t))
            {
                t.GetComponent<BasicEnemyScript>().Slow(0f);
                ListOfEnemiesInAura.Remove(t);
                
            }
                
        }
    }

    public override void ApplyAura(GameObject t)
    {
      if (!(t.GetComponent <BasicEnemyScript>().HasResist (Slow)))
        {
            t.GetComponent<BasicEnemyScript>().Slow (SlowPercentage) ;
        }
    }
    
}
