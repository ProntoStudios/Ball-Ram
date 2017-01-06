using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(h, v);
    }
}
