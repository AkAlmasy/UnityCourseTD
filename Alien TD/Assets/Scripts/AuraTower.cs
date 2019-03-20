using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AuraTower : MonoBehaviour {

    [SerializeField] public int Range;
    protected List<GameObject>  ListOfEnemiesInAura = new List<GameObject>();


    public abstract void ApplyAura(GameObject t);
    
    public abstract void InRange();

}
