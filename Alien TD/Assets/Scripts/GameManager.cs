using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Infos { PressForWave, NextWave, Escaped, NotEnoughGold, PlaceMiniGun,PlaceSlowTower,PlaceArcherTower,PlaceLaser,PlaceTurret2,PlaceMissile,ZombieLine,OrcLine };
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Wave> IncomingWaves = new List<Wave>();

    public List<GameObject> chests = new List<GameObject>();

    public List<GameObject> EnemiesAlive = new List<GameObject>();

  
        


    [SerializeField]
    public GameObject  Finish;
    public Text goldtext;
    public Text enemytext;
    public Text portaltext;
    public Text gameovertext;
    public Text infotext;
   
    public GameObject Spawnpoint;
    public GameObject TurretForPurchase;
    public Canvas HealthCanvas;
    public int CostOfTurret;


    public int EnemyCounter
    {
        get { return RemainingEnemy; }
        set { RemainingEnemy += value;
            ChangeEnemyCounterText();
        }
    }   


    
    int CurrentWaveCounter = 0;

    int PortalHealth;

    public int currentgold = 500;

    int RemainingEnemy;
    
    






    private void Awake()
    {
        Singleton();
        FindCanvasForHealthUI();
        ChangeGoldText();       
        Spawnpoint = GameObject.FindWithTag("Spawnpoint");
        ChangeInfoText("Press Space for Next Waves...");
    }



  

    private void Update()
    {
        IsVictory();
        IsPlayerReadyForNextWave();
    }



    private void IsPlayerReadyForNextWave()
    {
        if (Input.GetKeyUp(KeyCode.Space) && IncomingWaves.Count > CurrentWaveCounter )
        {
           
          

            StartCoroutine(IncomingWaves[CurrentWaveCounter++].Spawn());
            
            ChangeInfoText("Next Wave...");

        }
    }
  

    void IsVictory()
    {
        if (IncomingWaves.Count <=CurrentWaveCounter  && EnemyCounter <= 0)
        {
            GameOver("VICTORY");
        }
    }

 










    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }


    private void ChangeGoldText()
    {
        goldtext.text = "Gold:"+ currentgold.ToString();
    }
    public void ChangeEnemyCounterText()
    {
        enemytext.text = "Enemies:" + EnemyCounter.ToString();
    }

    public void GameOver(string text)
    {
        gameovertext.text = text;
        StopAllCoroutines();
        Time.timeScale = 0;


    }

    public void ChangePortalText(int health)
    {
        portaltext.text = "Health:" + health.ToString();

    }

    public bool IsEnougGoldForTower()
    {
        return currentgold >= CostOfTurret ;
    }

    public void ChangeGold(int gold)
    {
        currentgold += gold;
        ChangeGoldText();
    }

    public void EnemyKilledOrEscaped()
    {
        if (EnemyCounter >0)
        EnemyCounter=-1;
        
        ChangeEnemyCounterText();
    }

    public void BossAbility(int minions)
    {
        EnemyCounter = minions;
        ChangeEnemyCounterText();
    }

    


    public void ChangeInfoText(string info)
    {
        infotext.text = "Wave "+ CurrentWaveCounter+": " + info + "\n" + infotext.text;
    }

    public void FindCanvasForHealthUI()
    {
        foreach (Canvas c in FindObjectsOfType <Canvas>())
        {
            if (c.renderMode==RenderMode .WorldSpace)
            {
                HealthCanvas = c;
            }
                    }
    }




}



