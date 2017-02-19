using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelScript : MonoBehaviour {
	public static GameOverPanelScript instance;
	public bool isOn = false;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		gameObject.SetActive(false);

	}
	public IEnumerator TurnOnPanel(){
		if (!isOn) {
			isOn = true;
			gameObject.SetActive (true);
			for (float i = 0; i < 1f; i += 0.08f) {
				gameObject.GetComponent<CanvasRenderer> ().SetAlpha (i);
				yield return new WaitForSeconds (0.01f);
			}
			gameObject.GetComponent<CanvasRenderer> ().SetAlpha (1f);
			StartCoroutine (ExitButtonScript.instance.MoveIn ());
			StartCoroutine (RetryButtonScript.instance.MoveIn ());
			StartCoroutine (HighscoreTextScript.instance.MoveIn ());
		}
	}
	public IEnumerator TurnOffPanel(){
		if (isOn) {
			isOn = false;

			StartCoroutine (ExitButtonScript.instance.MoveOut ());
			StartCoroutine (RetryButtonScript.instance.MoveOut ());
			StartCoroutine (HighscoreTextScript.instance.MoveOut ());

			for (float i = 1f; i > 0f; i -= 0.1f) {
				gameObject.GetComponent<CanvasRenderer> ().SetAlpha (i);
				yield return new WaitForSeconds (0.01f);
			}
			gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0f);
			yield return new WaitForSeconds (2f);
			gameObject.SetActive (false);
		}
	}
}
