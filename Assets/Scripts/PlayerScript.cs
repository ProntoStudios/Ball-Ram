using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private Rigidbody2D rb2d;
	private CircleCollider2D playerCol;
	public int health;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		playerCol = GetComponent<CircleCollider2D> ();

		health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		if(health < 1){
			GameControl.instance.PlayerDied ();
		}
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Projectile") {
			health--;
			Debug.Log("Health: " + health);
		}
	}
}
