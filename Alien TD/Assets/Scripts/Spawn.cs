using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject monsterPrefab; 
	public int monstercounter = 10; 
	public float interval = 3; 
	int count = 0;

	// Use this for initialization
	void Start() {
		
		InvokeRepeating("SpawnNext", 0, interval);
	}

	void SpawnNext() {
		Instantiate(monsterPrefab, transform.position, Quaternion.identity);
		count++;
		if (count == monstercounter)
			CancelInvoke ("SpawnNext");
		}
}
