using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorGoldenTigerScript : BasicEnemyScript {

    [SerializeField] [Range(0, 1f)] float HealthPercentageToTriggerAbility;

    bool IsAbility;

         private void Update()
    {
        if (IsReachedDestination())
        {
            EnemyEscaped();
        }

        if (EnemyHealth.Hp / EnemyHealth.SetHealth < HealthPercentageToTriggerAbility)
            Ability();
        else if (GameManager .Instance .EnemiesAlive .Count<2&&IsAbility ==true)
        {
            gameObject.tag = "Enemy";
            agent.isStopped = false;
            anim.SetTrigger("IsRun");
            IsAbility = false;
            GameManager.Instance.ChangeInfoText("Emperor Golden Tiger: Continue!");
            MoveToLocation();
        }
    }
    public override void Ability()
    {
        gameObject.tag = "Untagged";
        agent.isStopped = true;
        anim.SetTrigger("AbilityTrigger");
        GameManager.Instance.ChangeInfoText("Emperor Golden Tiger: Restart!");
        HealthPercentageToTriggerAbility = -1000f;
        IsAbility = true;
        for (int i=0;i<GameManager .Instance .IncomingWaves .Count-1;i++)
        {
            StartCoroutine(GameManager.Instance.IncomingWaves[i].Spawn());
        }
    }


    public override void Die()
    {

        if (gameObject.tag == "Enemy")
        {

            gameObject.tag = "Untagged";
            agent.isStopped = true;
           

            GameManager.Instance.ChangeGold(Gold);
            GameManager.Instance.EnemyKilledOrEscaped();
            GameManager.Instance.EnemiesAlive.Remove(gameObject);

            Destroy(gameObject, 2f);
        }
    }

}
