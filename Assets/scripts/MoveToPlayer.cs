using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null) {
			GameController gameController = gameControllerObject.GetComponent<GameController>();
			rb = GetComponent<Rigidbody>();
			if (gameController.player != null) {
				Vector3 direction = (gameController.player.transform.position - transform.position).normalized;
				direction.y = 0;
				rb.velocity = direction * speed;
			}
		} else {
			Debug.Log ("Cannot find GameController script");
		}
	}
}
