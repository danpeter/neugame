using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayerController : MonoBehaviour {
	private Rigidbody rb;

	public int health;
	public float thrust;
	public float sideThrust;
	public float maxVelocity;
	public float brakeDrag;
	public float turnRate;
	public float tilt;
	public GameObject shot;
	public GameObject missile;
	public GameObject explosion;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	public float fireRateMissiles;
	private float nextFireMissiles;
	private AudioSource audioSource;
	private GameController gameController;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}

	void FixedUpdate () {

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		if (!Input.GetKey (KeyCode.LeftAlt)) {
			TurnToMouse ();
		}

		if (moveVertical != 0) {
			Accelerate (moveVertical);
		}
		if (moveHorizontal != 0) {
			Turn (moveHorizontal);
		}
		if (Input.GetKey (KeyCode.C)) {
			rb.drag = brakeDrag;
		} else {
			rb.drag = 0;
		}

		ClampVelocity ();
	}

	void TurnToMouse ()
	{
		Vector3 mousePositionScreen = Input.mousePosition;
		mousePositionScreen.z = 10; // Needed for perspective camera?
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (mousePositionScreen);
		Vector3 shipposition = transform.position;
		Vector3 dir = mousePosition - transform.position;
		dir.y = 0;
		Quaternion rot = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, turnRate * Time.deltaTime);
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}

		if (Input.GetButton ("Fire2") && Time.time > nextFireMissiles) {
			RaycastHit hitInfo = new RaycastHit ();
			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit && hitInfo.transform.gameObject.tag == "Enemy") {
				//Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
				nextFireMissiles = Time.time + fireRateMissiles;
				GameObject obj = (GameObject) Instantiate (missile, shotSpawn.position, shotSpawn.rotation);
				obj.GetComponent<TargetingMissile> ().SetTarget (hitInfo.transform);
				//audioSource.Play ();
			} else {
				//Debug.Log ("Miss...");
			}
		}

		UpdateHealthText ();
	}

	void Accelerate(float moveVertical)
	{
		rb.AddForce (transform.forward * thrust * Mathf.Sign(moveVertical));
	}

	void Turn(float moveHorizontal)
	{
		rb.AddForce (transform.right * sideThrust * Mathf.Sign (moveHorizontal));
	}

	void ClampVelocity ()
	{
		rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxVelocity, maxVelocity), 0.0f, Mathf.Clamp (rb.velocity.z, -maxVelocity, maxVelocity));
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "EnemyWeapon") {
			Weapon weapon = other.gameObject.GetComponent<Weapon> ();
			health -= weapon.damage;
			if (weapon.explosion != null) {
				Instantiate (weapon.explosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
			}
			Destroy (other.gameObject);
		}

		if (health <= 0) {
			Instantiate(explosion, transform.position, transform.rotation);
			gameController.GameOver ();
			Destroy (gameObject);
		}
	}
	void UpdateHealthText() {
		gameController.healthText.text = "Health: " + health;
	}
}
