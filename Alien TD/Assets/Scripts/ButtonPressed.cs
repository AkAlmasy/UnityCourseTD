using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPressed : MonoBehaviour {



    public void RestartTheGame()
    {
        SceneManager.LoadScene("MapB");
        Time.timeScale =1;
    }


    public void ButtonClicked()
    {
      /*  bool enoughgold = GameManager.Instance.IsEnougGoldForTower();
        if (enoughgold)
            GameManager.Instance.ShowTowerPlaces(true);
        else
            GameManager.Instance.ChangeInfoText((int)Infos.NotEnoughGold );*/
    }
}
