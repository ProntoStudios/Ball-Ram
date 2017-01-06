using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGame : MonoBehaviour {
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate () {

		/*
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
		Debug.Log (h + "" + v);

		rb2d.velocity = new Vector2(h, v);
		*/

		gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x + 0.01f, gameObject.transform.localPosition.y + 0.01f, gameObject.transform.localPosition.z);


    }
}
