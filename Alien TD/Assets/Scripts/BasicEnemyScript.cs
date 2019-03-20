using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasicEnemyScript : MonoBehaviour {

    [SerializeField]
    public float startSpeed = 1f;
    [SerializeField]
    public float Speed
    {
        get { return agent.speed; }
        set { agent.speed = value; }
    }
    
    [SerializeField]
     public Resist SlowRes;
    [SerializeField] protected int Gold = 50;
    [SerializeField]
    protected List<Resist> resists = new List<Resist>();
    [SerializeField] protected Health EnemyHealth ;
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected Animator anim=null;
   
    [SerializeField]
    int DestinationDifference = 1;

    private void Start()
    {
        int RND = Random.Range(0, 100);
        if (RND > 50)
        {

            resists.Add(SlowRes);

        }
        Speed = startSpeed;
        AddThisToEnemyList();

        MoveToLocation();
    }


    private void Update()
    {
        if (IsReachedDestination())
        {
            EnemyEscaped();           
        }
    }

    protected void EnemyEscaped()
    {
        GameManager.Instance.Finish.GetComponent<PortalManager>().PortalHealth = -1;
        GameManager.Instance.ChangeInfoText("Enemy Escaped!");
        EnemyHealth.CloseHPUI();
        Destroy(gameObject);
    }

    protected bool IsReachedDestination()
    {
        return (Vector3.Distance(transform.position, GameManager.Instance.Finish.transform.position) < DestinationDifference);

    }
    public bool HasResist(Resist res)
    {
        if (resists.Contains(res)) return true;
        return false;
    }
   

    protected void AddThisToEnemyList()
    {
        GameManager.Instance.EnemiesAlive.Add(gameObject);
    }
    public void MoveToLocation()
    {

        agent.SetDestination(GameManager.Instance.Finish.transform.position);
        agent.speed = Speed;
    }
    public void Slow(float pct)
    {
        Speed = startSpeed * (1f - pct);
    }
    public void LoseHealth(float damagePoint)
    {
        EnemyHealth.Hp = -damagePoint;
        
        if (EnemyHealth.Hp <= 0)
            Die();
        
    }

    public abstract void Die();
    public abstract void Ability();
}
