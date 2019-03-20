using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : BasicEnemyScript {

    [SerializeField] float Range;

    public AudioClip deathSound;
    AudioSource audioSource;

    

    public override void Ability()
    {
        int RND = Random.Range(0,100) ;
        if (RND > 50)
        {
            StartCoroutine(DelayedRespawn());
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(deathSound, 0.7f);
            Destroy(gameObject,3f);
        }
    }

    
    public override void Die()
    {
        if (gameObject.tag == "Enemy")
        {
        
            gameObject.tag = "Untagged";
            agent.isStopped = true;
            anim.SetTrigger("IsDead");

            GameManager.Instance.ChangeGold(Gold);
            GameManager.Instance.EnemyKilledOrEscaped();
            GameManager.Instance.EnemiesAlive.Remove(gameObject);
            
            Ability();
        }
    }


    IEnumerator DelayedRespawn()
    {
        yield return new WaitForSeconds(4f);
        anim.SetTrigger("LiveAgain");
        gameObject.tag = "Enemy";
        EnemyHealth.Hp = EnemyHealth.SetHealth;
        AddThisToEnemyList();
        agent.isStopped = false;
        GameManager.Instance.ChangeInfoText("Zombie: I live Again!");
        GameManager.Instance.EnemyCounter = 1;

    }
   
}
