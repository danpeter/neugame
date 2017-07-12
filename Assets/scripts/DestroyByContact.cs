using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find GameController script");
		}
	}
	 
	void OnTriggerEnter(Collider other) 
	{
		
	/*	if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "PlayerWeapon" && this.tag == "Player") {
			return;
		}
		if (other.tag == "EnemyWeapon" && this.tag == "Enemy") {
			return;
		}
		if (other.tag == "Enemy" && this.tag == "Player") {
			return;
		}
		if (other.tag == "Player" && this.tag == "Enemy") {
			return;
		}

		Instantiate(explosion, transform.position, transform.rotation);

		if (this.tag == "Player") {
			gameController.GameOver ();
		}
			
		Destroy(gameObject);
		*/
		Debug.Log (other.name);
		if (other.tag == "AsteroidGenerator") {
			return;
		}

		if (other.tag == "Player") {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}

		if (other.tag == "Enemy") {
			Instantiate(explosion, other.transform.position, other.transform.rotation);
		}

		if (other.tag == "EnemyWeapon" || other.tag == "PlayerWeapon") {
			Weapon weapon = other.gameObject.GetComponent<Weapon> ();
			if (weapon.explosion != null) {
				Instantiate (weapon.explosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
			}
		}



		Destroy(other.gameObject);



	}
}
