using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMissile : MonoBehaviour {

	public float speed;
	public float turnRate;
	public Transform target;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		if (target != null) {
			Vector3 direction = (target.position - transform.position);
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation (direction);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turnRate);
		}

		transform.position += transform.forward * Time.deltaTime * speed;
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}
}
