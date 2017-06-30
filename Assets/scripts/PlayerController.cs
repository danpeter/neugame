﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMax, zMin;
}

public class PlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	public float maxSpeed;
	public float acceleration;
	public float turnRate;
	public float tilt;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	private AudioSource audioSource;

	public Boundary boundary;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
	}
		
	void FixedUpdate () {

		float moveVertical = Input.GetAxis ("Vertical");
		if (moveVertical != 0) {
			Accelerate ();
		}

		Vector3 mousePositionScreen = Input.mousePosition;
		mousePositionScreen.z = 20;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePositionScreen);
		//Debug.Log ("Mouse: " + mousePosition);

		Vector3 shipposition = transform.position;
		//Debug.Log ("Ship: " + shipposition);

		Vector3 dir = mousePosition - transform.position;
		dir.y = 0;
		Quaternion rot = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnRate * Time.deltaTime);

		Debug.Log ("Speed: " + speed);

	}


	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}


	}

	void Accelerate() {
		speed += acceleration * Time.deltaTime;
		speed = Mathf.Clamp (speed, 1, maxSpeed);
		//transform.Translate (speed * Time.deltaTime, 0, 0, Space.World);
		transform.position += transform.forward * Time.deltaTime * speed;
	}


}
