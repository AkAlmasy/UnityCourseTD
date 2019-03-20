using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour {




    int TurretCost; 
    bool ActivatedTower = false;
    GameObject ActiveTower;

    private void OnMouseDown()
    {
        if (!ActivatedTower&&GameManager .Instance .IsEnougGoldForTower () && GameManager.Instance.CostOfTurret > 0)
        {
            TurretCost = GameManager.Instance.CostOfTurret;
            //Ray mouseposition = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //Physics.Raycast(mouseposition, out hit);
            
                
                Debug.Log(gameObject.name);
            ActiveTower= Instantiate(GameManager.Instance.TurretForPurchase , gameObject.transform.position , Quaternion.identity);
            GameManager.Instance.ChangeGold(-TurretCost );
            GameManager.Instance.CostOfTurret = 0;
            ActivatedTower = true;
        }
        else
        {
            if (!GameManager.Instance.IsEnougGoldForTower()&& GameManager.Instance.CostOfTurret >0)
            {
                GameManager.Instance.ChangeInfoText("Not Enough Gold!");
            }
            if (ActivatedTower )
            { Destroy(ActiveTower);
                GameManager.Instance.ChangeGold(TurretCost );
                ActivatedTower = false;
            }
        }
    }
}
