using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splitScript : MonoBehaviour {
	private ProjectileScript proj;

	// Use this for initialization
	void Start () {
		proj = GetComponent<ProjectileScript> ();
		proj.health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Shield") {
			proj.health--;
			if (proj.health <= 2) {
				for (int i = 0; i < 4; i++) {
					GameControl.instance.spawnProj (2, gameObject.transform.position);
				}
				GameControl.instance.deleteProj (gameObject);
			}
		}

	}
}
