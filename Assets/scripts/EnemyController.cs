﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float turnRate;
	public float thrust;
	public float maxVelocity;

	public float attackDistance;
	public float delay;
	public float fireRate;

	public GameObject shot;
	public Transform shotSpawn;

	private Rigidbody rb;
	private Transform target;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		rb = GetComponent<Rigidbody>();

		if (gameControllerObject != null) {
			GameController gameController = gameControllerObject.GetComponent<GameController>();
			if (gameController.player != null) {
				target = gameController.player.transform;
			}
		} else {
			Debug.Log ("Cannot find GameController script");
		}

		InvokeRepeating ("Fire", delay, fireRate);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target != null) {

			Vector3 direction = (target.position - transform.position);
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation (direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnRate);

			float distance = Vector3.Distance (transform.position, target.position);

			if (distance < attackDistance) {
				rb.drag = 3; // brake
				rb.AddForce (transform.right * thrust * 2);
			} else {
				rb.drag = 0;
				rb.AddForce (transform.forward * thrust);
				//transform.position += transform.forward * Time.deltaTime * speed;
			}
			ClampVelocity();
		}
	}

	void ClampVelocity ()
	{
		rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxVelocity, maxVelocity), 0.0f, Mathf.Clamp (rb.velocity.z, -maxVelocity, maxVelocity));
	}

	void Fire()
	{
		if (target != null) {
			float distance = Vector3.Distance (transform.position, target.position);
			Debug.Log ("Distance: " + distance);
			if (distance < attackDistance) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource> ().Play ();
			}
		}
	}
}
