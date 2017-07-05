using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMissile : MonoBehaviour {

	public float thrust;
	public float turnRate;
	public float maxVelocity;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		Vector3 direction = (new Vector3(0, 0, 0) - transform.position);
		direction.y = 0;
		Quaternion rotation = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnRate);

		rb.AddForce (transform.forward * thrust);
		//transform.position += transform.forward * Time.deltaTime * thrust;
		ClampVelocity();
	}

	void ClampVelocity ()
	{
		rb.velocity = new Vector3 (Mathf.Clamp (rb.velocity.x, -maxVelocity, maxVelocity), 0.0f, Mathf.Clamp (rb.velocity.z, -maxVelocity, maxVelocity));
	}
}
