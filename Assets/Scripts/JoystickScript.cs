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
	//static Vector2 centerPos;
	static float joystickScaler;
	static Vector2 joystickScalerVec;
	/*
	static Vector2 joystickScalerVecNeg;
	static Vector2 joystickScalerVecInv;
	*/
	bool isTouching = false;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		joystickScaler = Screen.width / 16f;
		joystickCenter = gameObject.transform.position;

		//centerPos = new Vector2 (Screen.height/2f, Screen.width/2f);

		joystickScalerVec = new Vector2(joystickScaler, joystickScaler);
		/*
		joystickScalerVecNeg = new Vector2(-joystickScaler, -joystickScaler);
		joystickScalerVecInv = new Vector2(1f/joystickScaler, 1f/joystickScaler);
		curTouchPos = Vector2.zero;
		*/
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.touchCount > 0) {
			curTouchPos = Input.GetTouch (0).position;
			relTouchPos.Set (curTouchPos.x - joystickCenter.x, curTouchPos.y - joystickCenter.y);

			if (relTouchPos.magnitude >= joystickScaler) {
				relTouchPos.Normalize ();
				relTouchPos.Scale (joystickScalerVec);
			}
			joystickCenterScript.instance.transform.position = relTouchPos + joystickCenter;
			movePos = relTouchPos.normalized;
			isTouching = true;
		} else if (isTouching) {
			isTouching = false;
			joystickCenterScript.instance.transform.position = joystickCenter;
			movePos = Vector2.zero;
		}
		/*
		int numTouch = Input.touchCount;
		if (numTouch == 0) {
			isTouching = false;
			gameObject.transform.position = new Vector2(Screen.width*2, Screen.height*2);
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
					movePos.Scale (joystickScalerVec);
				}
				joystickCenterScript.instance.transform.position = movePos + joystickCenter;
				movePos.Scale (joystickScalerVecInv);
			} else {
				//creates a shifted jouystick
				if (relTouchPos.magnitude > joystickScaler) {
					isTouching = true;
					joystickOffset = Vector2.Scale(relTouchPos.normalized, (joystickScalerVecNeg));
					joystickCenter = curTouchPos + joystickOffset;
					gameObject.transform.position = joystickCenter;
				}
			}
		}
		*/

	}
	private void OnGUI(){
		GUI.Label (new Rect (10, 150, 100, 50), "relPos: " + relTouchPos);
		GUI.Label (new Rect (10, 100, 100, 50), "Move: " + movePos);	
	}
}		