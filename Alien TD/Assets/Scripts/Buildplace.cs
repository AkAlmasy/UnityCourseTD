using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildplace : MonoBehaviour {
	// tower
	public GameObject towerPrefab;

	void OnMouseUpAsButton(){
		GameObject g = (GameObject)Instantiate(towerPrefab);
		g.transform.position = transform.position + Vector3.up;
	}
}
