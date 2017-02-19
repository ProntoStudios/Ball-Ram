using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePanelScript : MonoBehaviour {
	public static ContinuePanelScript instance;
	private GameObject continuePanel;
	public bool isOn = false;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		continuePanel = gameObject;
		continuePanel.SetActive(false);

	}
	public IEnumerator TurnOnPanel(){
		if (!isOn) {
			isOn = true;
			continuePanel.SetActive (true);
			for (float i = 0; i < 1f; i += 0.08f) {
				continuePanel.GetComponent<CanvasRenderer> ().SetAlpha (i);
				yield return new WaitForSeconds (0.01f);
			}
			continuePanel.GetComponent<CanvasRenderer> ().SetAlpha (1f);

			StartCoroutine (RetryButtonScript.instance.MoveIn ());
			StartCoroutine (ContinueTextScript.instance.MoveIn ());
		}
	}
	public IEnumerator TurnOffPanel(){
		if (isOn) {
			isOn = false;
			StartCoroutine (RetryButtonScript.instance.MoveOut ());
			StartCoroutine (ContinueTextScript.instance.MoveOut ());

			for (float i = 1f; i > 0f; i -= 0.1f) {
				continuePanel.GetComponent<CanvasRenderer> ().SetAlpha (i);
				yield return new WaitForSeconds (0.01f);
			}
			continuePanel.GetComponent<CanvasRenderer> ().SetAlpha (0f);
			yield return new WaitForSeconds (2f);
			continuePanel.SetActive (false);
		}
	}


}
