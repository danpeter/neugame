using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour {

	public GameObject[] asteroids;
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
			Vector2 spawnPoint2d = Random.insideUnitCircle * colliderRadius;
			Vector3 spawnPoint3d = new Vector3(spawnPoint2d.x, 0, spawnPoint2d.y);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (asteroids[Random.Range(0, 3)], spawnPoint3d, spawnRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		//The collider should follow the player ship
		if (player != null) {
			transform.position = player.transform.position + offset;
		}
	}

	void OnTriggerExit(Collider other) 
	{
		//When a asteroid leaves the collider we want to move it back in outside the players vision, on the edge of the collider
		if (other.tag == "Asteroid") {
			Debug.Log ("Moving asteroid");
			//Normalize makes into a "onUnitCircle, kindof
			Vector2 newPosition2d = Random.insideUnitCircle.normalized;
			Vector3 newPosition = new Vector3(newPosition2d.x, 0, newPosition2d.y) * colliderRadius;
			other.transform.position = transform.position + newPosition;
		}

	}
}
