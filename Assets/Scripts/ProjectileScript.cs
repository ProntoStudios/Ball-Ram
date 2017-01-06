using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	private Rigidbody2D rb2d;
	private CircleCollider2D projCol;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		projCol = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Shield") {
			GameObject.Destroy (gameObject);
		}
	}
}
