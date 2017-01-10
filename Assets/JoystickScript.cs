using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickScript : MonoBehaviour {
	Vector2 curTouchPos;
	Vector2 relTouchPos;
	Vector2 relTouchPosNorm;
	static Vector2 centerPos = new Vector2 (Screen.height/2f, Screen.width/2f);
	bool isTouching = false;
	// Use this for initialization
	void Start () {
		curTouchPos = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		int numTouch = Input.touchCount;
		if (numTouch == 0) {
			isTouching = false;
			gameObject.transform.position = Vector2.zero;

		}
		else if (numTouch > 0) {
			curTouchPos = Input.GetTouch (0).position;
			relTouchPos.Set (curTouchPos.x - centerPos.x, curTouchPos.y - centerPos.y);
			relTouchPosNorm = relTouchPos.normalized;

			if (isTouching) {

			} else {
				isTouching = true;
				//TODO: SET LOCATION OF JOYSTICK
			}
			Debug.Log (centerPos);
			Debug.Log (curTouchPos);
			Debug.Log (relTouchPos);
		}

	}
}
