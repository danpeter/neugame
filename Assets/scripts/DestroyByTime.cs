using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		StartCoroutine ("AutoDestroy");
	}

	void DestroySelf()
	{
		if (explosion != null) {
			Instantiate (explosion, transform.position, transform.rotation);
		}
		Destroy (gameObject);
	}

	IEnumerator AutoDestroy ()
	{
		yield return new WaitForSeconds (lifetime);
		DestroySelf ();
	}
}
