using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HungrySpiderScript : BasicEnemyScript
{



    [SerializeField] float EatingRange;

    public AudioClip deathsound;
    AudioSource audioSource;

    private void Update()
    {
        if (IsReachedDestination())
        {
            EnemyEscaped();
            GameManager.Instance.EnemiesAlive.Remove(gameObject);
        }
        Ability();
    }


    public override void Ability()
    {
        int tempcounter = GameManager.Instance.chests.Count;
        foreach (var c in GameManager.Instance.chests)
        {

            float Distance = Vector3.Distance(transform.position, c.transform.position);
            if (Distance < EatingRange && EnemyHealth.Hp > 0f)
            {
                GameManager.Instance.chests.Remove(c);
                Destroy(c);
                GameManager.Instance.ChangeInfoText("Spider: Hamm!");
                anim.SetTrigger("IsEating");
                gameObject.transform.localScale = gameObject.transform.localScale * 2;
                EnemyHealth.Hp = EnemyHealth.SetHealth;
                EnemyHealth.SetHealth = EnemyHealth.SetHealth * 2;
            }
            if (tempcounter > GameManager.Instance.chests.Count)
                break;
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
            Destroy(EnemyHealth);
            audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(deathsound, 0.7f);
            Destroy(gameObject, 2f);
        }

    }



}
