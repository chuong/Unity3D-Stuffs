using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public Boundary boundary;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireDelta = 0.5f;

	private Rigidbody rb;
	private float nextFire = 0.5f;
	private float myTime = 0.0f;
	private AudioSource audio;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		speed = 10.0f;
		audio = GetComponent<AudioSource> ();
	}

	void Update()
	{
		myTime = myTime + Time.deltaTime;
		if (Input.GetButton("Fire1") && myTime > nextFire)
		{
			nextFire = myTime + fireDelta;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

			nextFire = nextFire - myTime;
			myTime = 0.0F;

			audio.Play ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.09f, 0.0f, rb.velocity.x * -tilt);
	}
}
