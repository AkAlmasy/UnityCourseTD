using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour {

    public  int PortalHealth {
        get { return _PortalHealth; }
        set { _PortalHealth +=  value;
            IsDefeat();
            
            
            GameManager.Instance.ChangePortalText(_PortalHealth);
            GameManager.Instance.EnemyKilledOrEscaped();
        }
    }


    int  _PortalHealth ;

    
    private void Start()
    {
        PortalHealth =10;
        GameManager.Instance.ChangePortalText(PortalHealth);
    }

   

    public void IsDefeat()
    {
       if (PortalHealth <= 0)
            GameManager.Instance.GameOver("DEFEAT");
    }
    
}
