using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickCenterScript : MonoBehaviour {
	public static joystickCenterScript instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
