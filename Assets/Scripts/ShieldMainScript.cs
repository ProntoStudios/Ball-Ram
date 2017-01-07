using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMainScript : MonoBehaviour {
	
	public float rotateSpeed;
	private Rigidbody2D rb2d;
	private BoxCollider2D shieldCol; 

	private Vector3 zAxis = new Vector3 (0,0,1);

	// Use this for initialization
	void Start () {
		//rotateSpeed = 2f;
		rotateSpeed = 2f;
		rb2d = GetComponent<Rigidbody2D> ();
		shieldCol = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var playerPos = GameObject.Find ("Player").transform.position;
		gameObject.transform.RotateAround ((Vector3) playerPos, zAxis, rotateSpeed);
		//gameObject.transform.localScale = (Vector3)gameObject.transform.localScale + new Vector3 (0.1f,0,0);

	}
}
