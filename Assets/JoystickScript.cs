using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickScript : MonoBehaviour {
	public static JoystickScript instance;
	Vector2 curTouchPos;
	Vector2 relTouchPos;
	public Vector2 movePos;
	Vector2 joystickOffset;
	Vector2 joystickCenter;
	static Vector2 centerPos = new Vector2 (Screen.height/2f, Screen.width/2f);
	static float joystickScaler = Screen.width / 10f;
	bool isTouching = false;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		curTouchPos = Vector2.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int numTouch = Input.touchCount;
		if (numTouch == 0) {
			isTouching = false;
			gameObject.transform.position = new Vector2(Screen.width, Screen.height);
			movePos = Vector2.zero;
		}
		else if (numTouch > 0) {
			curTouchPos = Input.GetTouch (0).position;
			relTouchPos.Set (curTouchPos.x - centerPos.x, curTouchPos.y - centerPos.y);
			//TODO: DISABLE IF FINGER TOO CLOSE TO CENTER
			if (isTouching) {
				//adjusts movement while still touching the screen;
				movePos = curTouchPos-joystickCenter;
				//TODO: LIMIT MAGNITUTE;
				if (movePos.magnitude >= joystickScaler) {
					movePos.Normalize ();
					movePos.Scale (new Vector2(joystickScaler, joystickScaler));
				}
				movePos.Scale (new Vector2(1f/joystickScaler, 1f/joystickScaler));
			} else {
				//creates a shifted jouystick
				if (relTouchPos.magnitude > joystickScaler) {
					isTouching = true;
					joystickOffset = Vector2.Scale(relTouchPos.normalized, (new Vector2 (-joystickScaler, -joystickScaler)));
					joystickCenter = curTouchPos + joystickOffset;
					gameObject.transform.position = joystickCenter;
				}
			}
		}

	}
	private void OnGUI(){
		GUI.Label (new Rect (10, 150, 100, 50), "relPos: " + relTouchPos);
		GUI.Label (new Rect (10, 100, 100, 50), "Move: " + movePos);	
	}
}		