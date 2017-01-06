using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	private CircleCollider2D projCol;
	private int minSpeed, maxSpeed;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		projCol = GetComponent<CircleCollider2D> ();
		minSpeed = 1;
		maxSpeed = 10;
		rb2d.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Shield") {
			GameObject.Destroy (gameObject);
		}
	}
}
