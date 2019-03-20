using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportingOrcScript : BasicEnemyScript
{

    [SerializeField] TeleportPoints PointsToTeleport;

    List<Transform> TransformsForTeleport = new List<Transform>();


    float TeleportPointRange = 1f;



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
        Transform temporarytf = null;
        if (TransformsForTeleport.Count == 0)
        {
            foreach (GameObject p in PointsToTeleport.Points)
                TransformsForTeleport.Add(p.transform);
        }
        else
            foreach (Transform t in TransformsForTeleport)
            {
                int NOPoints = TransformsForTeleport.Count;
                int percentage = Random.Range(0, 100);
                float Distance = Vector3.Distance(transform.position, t.position);
                if (Distance < TeleportPointRange && percentage > 1 && (temporarytf != t))
                {
                    int RND = Random.Range(0, NOPoints);
                    transform.position = TransformsForTeleport[RND].position;
                    GameManager.Instance.ChangeInfoText("Orc:Teleport!");
                }
                if (Distance < TeleportPointRange)
                    temporarytf = t;
            }
        TransformsForTeleport.Remove(temporarytf);
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

            Destroy(gameObject, 2f);
        }
    }
}
