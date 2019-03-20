using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teleport")]
public class TeleportPoints : ScriptableObject {

    [SerializeField] public List<GameObject > Points = new List<GameObject >();
}
