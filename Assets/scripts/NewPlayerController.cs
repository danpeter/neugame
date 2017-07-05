using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {
	private Rigidbody rb;
	public float thrust;
	public float sideThrust;
	public float maxVelocity;
	public float brakeDrag;
	public float turnRate;
	public float tilt;
	public GameObject shot;
	public GameObject missile;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	public float fireRateMissiles;
	private float nextFireMissiles;
	private AudioSource audioSource;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {

		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		TurnToMouse ();

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

		//TurnToMouse ();

		//Debug.Log ("Speed: " + speed);

		//rb.position += transform.forward * Time.deltaTime * speed;
	}

	void TurnToMouse ()
	{
		Vector3 mousePositionScreen = Input.mousePosition;
		mousePositionScreen.z = 10; // Needed for perspective camera?
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (mousePositionScreen);
		//Debug.Log ("Mouse: " + mousePosition);
		Vector3 shipposition = transform.position;
		//Debug.Log ("Ship: " + shipposition);
		Vector3 dir = mousePosition - transform.position;
		dir.y = 0;
		Quaternion rot = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, turnRate * Time.deltaTime);
		//transform.rotation = Quaternion.RotateTowards (transform.rotation, rot, turnRate * Time.deltaTime);
		//transform.rotation = rot;
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}

		if (Input.GetButton ("Fire2") && Time.time > nextFireMissiles) {
			nextFireMissiles = Time.time + fireRateMissiles;
			Instantiate (missile, shotSpawn.position, shotSpawn.rotation);
			//audioSource.Play ();
		}
	}

	void Accelerate(float moveVertical)
	{
		rb.AddForce (transform.forward * thrust * Mathf.Sign(moveVertical));
		//rb.position += transform.forward * Time.deltaTime * thrust;
	}

	void Turn(float moveHorizontal)
	{
		//if (Input.GetKey (KeyCode.LeftShift)) {
			//TurnToMouse ();
			rb.AddForce (transform.right * sideThrust * Mathf.Sign (moveHorizontal));
		//} else {
		//	rb.transform.Rotate (Vector3.up * turnRate * Time.deltaTime * Mathf.Sign (moveHorizontal));
		//}
	}

	void ClampVelocity ()
	{
		rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxVelocity, maxVelocity), 0.0f, Mathf.Clamp (rb.velocity.z, -maxVelocity, maxVelocity));
	}
}
