using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidGenerator : MonoBehaviour {

	public GameObject asteroid;
	public int asteroidCount;
	public GameObject player;
	private SphereCollider myCollider;
	private float colliderRadius;
	private Vector3 offset;


	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;

		myCollider = transform.GetComponent<SphereCollider>();
		colliderRadius = myCollider.radius;

		for (int i = 0; i < asteroidCount; i++) {
			Vector3 spawnPoint = Random.insideUnitSphere * colliderRadius;
			spawnPoint.y = 0; //Make into 2D coordinate
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (asteroid, spawnPoint, spawnRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		if (player != null) {
			transform.position = player.transform.position + offset;
		}
	}

	void OnTriggerExit(Collider other) 
	{
		//When a asteroid leaves the collider we want to move it back in, but outside the players vision
		if (other.tag == "Asteroid") {
			Debug.Log ("Moving asteroid");
			//Move the asteroid to the edge of the collider, centered on the player
			Vector3 newPosition = Random.onUnitSphere * colliderRadius;
			newPosition.y = 0; //Make into 2D coordinate
			other.transform.position = transform.position + newPosition;
		}

	}
}
