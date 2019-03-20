using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 10f;
	public GameObject impactEffect;
    [SerializeField] public int Damage = 20;

    private Transform target;




	public void Seek (Transform _target)
	{
		target = _target;
	}

	void Update ()
	{
		if (target == null) 
		{
			Destroy (gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) 
		{
			HitTarget ();
			return;
		}

		transform.Translate (dir.normalized * distanceThisFrame, Space.World);
	}

	void HitTarget ()
	{
		GameObject effectIns = (GameObject) Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (effectIns, 2f);
        //metódus átírva
        
		target.GetComponent<BasicEnemyScript> ().LoseHealth (Damage);
		Destroy (gameObject);
	}
}
