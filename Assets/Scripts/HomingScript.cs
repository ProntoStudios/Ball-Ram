﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingScript : MonoBehaviour {
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		var playerPos = GameObject.Find ("Player").transform.position;
		Vector3 toPlayer = playerPos - gameObject.transform.position;
		toPlayer.Normalize ();

		rb2d.velocity = (Vector3) rb2d.velocity + Vector3.ClampMagnitude(toPlayer,0.7f);
		rb2d.velocity = Vector3.ClampMagnitude (rb2d.velocity, 10);
		Vector3 targetDir = PlayerScript.instance.transform.position - transform.position;

		float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
