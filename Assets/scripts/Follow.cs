using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	public float parallax;

	private MeshRenderer mr;

	// Use this for initialization
	void Start () {
		mr = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Material mat = mr.material;

		Vector2 offset = mat.mainTextureOffset;

		offset.x = transform.position.x / transform.localScale.x * parallax;
		offset.y = transform.position.z / transform.localScale.y * parallax;

		//offset.x += Time.deltaTime / 10.0f;
		//offset.y += Time.deltaTime / 10.0f;

		mat.mainTextureOffset = offset;
	}
}
