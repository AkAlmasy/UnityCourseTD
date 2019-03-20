using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    private void OnMouseDown()
    {
        GameManager.Instance.ChangeGold(100);
        Destroy(gameObject);
    }
}
