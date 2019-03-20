using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour {

    [SerializeField]
    int Gold;

    private void OnMouseDown()
    {
        GameManager.Instance.ChangeGold(Gold);
        GameManager.Instance.chests.Remove(gameObject);
        Destroy(gameObject);
    }
}
