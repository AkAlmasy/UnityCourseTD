using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntAlien : BasicEnemyScript {


    public AudioClip deathSound;
    public AudioClip goldChest;
    AudioSource audioSource;

    
    
  
    
 
    [SerializeField]  GameObject Reward;




    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        int RND = Random.Range(0, 100);
        if (RND > 50)
        {

            resists.Add(SlowRes);

        }
        Speed = startSpeed;
        AddThisToEnemyList();

        MoveToLocation();
    }






    public override void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            Ability();
            gameObject.tag = "Untagged";
            agent.isStopped = true;
            anim.SetTrigger("IsDead");

            GameManager.Instance.ChangeGold(Gold);
            GameManager.Instance.EnemyKilledOrEscaped();
            GameManager.Instance.EnemiesAlive.Remove (gameObject);
            audioSource.PlayOneShot(deathSound, 0.7F);
            Destroy(gameObject, 3f);
        }
    }
    public override void Ability()
    {
        int Rnd = Random.Range(0, 100);
        if (Rnd > 50)
        {
          var chest=  Instantiate(Reward, gameObject.transform.position, Quaternion.identity);
            GameManager.Instance.chests.Add(chest);
            audioSource.PlayOneShot(goldChest, 0.7F);
        }
    }


}
