using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="Wave")]
public class Wave : ScriptableObject  {

    public List<GameObject> Enemies = new List<GameObject>();
     
    
   

   

    public IEnumerator  Spawn()
    {
        int EnemyIndex = 0;
        GameManager.Instance.EnemyCounter = Enemies.Count;
        while (Enemies.Count > EnemyIndex )
        {
           
            
            Instantiate(Enemies[EnemyIndex++], GameManager .Instance .Spawnpoint.transform.position, Quaternion.identity);
           
            yield return new WaitForSeconds(3f);
            
        }

        yield return null;




    }
}
