using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public float speed;

	private Rigidbody rbLaser;

	// Use this for initialization
	void Start () {
		rbLaser = GetComponent<Rigidbody> ();
		rbLaser.velocity = transform.forward * speed;
	}
	
}
