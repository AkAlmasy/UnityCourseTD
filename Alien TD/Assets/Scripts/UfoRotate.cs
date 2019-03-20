using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoRotate : MonoBehaviour {

    public float rotateSpeed = 40;

		
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
    }
}
