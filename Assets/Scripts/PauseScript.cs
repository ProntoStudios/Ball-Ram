using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	public static PauseScript instance;
	public bool paused = false;
	private GameObject pausePanel;
	void Start()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		pausePanel = gameObject;
		pausePanel.SetActive(false);
	}
	public void SwitchState(){
		if (paused) {
			paused = false;
			ContinueGame ();
		} else {
			paused = true;
			PauseGame ();
		}
	}
	private void PauseGame()
	{
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		//Disable scripts that still work while timescale is set to 0
	} 
	private void ContinueGame()
	{
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		//enable the scripts again
	}
}

