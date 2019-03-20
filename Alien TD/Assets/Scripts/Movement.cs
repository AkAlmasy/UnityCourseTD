using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    private Vector3     destination;
    private NavMeshAgent agent;
    public int speed;
    private void OnMouseDown()
    {
        Debug.Log("Click");
    }
    // Use this for initialization
    void Awake () {
        agent = GetComponent<NavMeshAgent>();
        // Portal vector3 paramétere hardcodolva
        destination =  new Vector3(-14f,0.5f,2.5f);
        MoveToLocation();
        
            
    }
   
    public void MoveToLocation()
    {
        agent.SetDestination(destination  );
        agent.speed = speed;
        
        //agent.isStopped = false;
    }
   public void Die()
    {
        Destroy(gameObject);
    }
}
