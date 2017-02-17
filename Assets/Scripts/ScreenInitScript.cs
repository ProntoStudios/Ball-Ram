using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInitScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
