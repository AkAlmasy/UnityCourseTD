using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {
   

    [SerializeField] List<GameObject> TowersList = new List<GameObject>();

    public void PurchaseTower(TowerButtonSO SO)
    {
        GameManager.Instance.CostOfTurret = SO.GoldCost;
        GameManager.Instance.TurretForPurchase = TowersList [SO.IndexOfTower ];
        if (GameManager.Instance.IsEnougGoldForTower())
            GameManager.Instance.ChangeInfoText("Place the Tower.");
        else GameManager.Instance.ChangeInfoText("Not Enough Gold!");
    }

    
}
