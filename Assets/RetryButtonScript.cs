using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButtonScript : MonoBehaviour {
	public static RetryButtonScript instance;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		
	}
	public IEnumerator MoveIn(){
		float speed = 23f;
		float accel = -1.2f;
		while (speed > -3f) {
			gameObject.transform.position = new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + speed);
			speed += accel;
			yield return new WaitForSeconds (0.005f);
		}
	}
}
