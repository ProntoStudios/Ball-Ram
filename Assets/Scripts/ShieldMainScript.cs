using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMainScript : MonoBehaviour {
	
	public float rotateSpeed;
	private Rigidbody2D rb2d;
	private BoxCollider2D shieldMainCol; 

	private Vector3 zAxis = new Vector3 (0,0,1);

	// Use this for initialization
	void Start () {
		rotateSpeed = 10f;
		rb2d = GetComponent<Rigidbody2D> ();
		shieldMainCol = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (Vector3.zero, zAxis, rotateSpeed);
		

	}
}
