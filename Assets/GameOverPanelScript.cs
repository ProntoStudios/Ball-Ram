using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelScript : MonoBehaviour {
	public static GameOverPanelScript instance;
	private GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		gameOverPanel = gameObject;
		gameOverPanel.SetActive(false);

	}
	public IEnumerator TurnOnPanel(){
		gameOverPanel.SetActive (true);
		for(float i = 0; i < 1f; i+= 0.08f){
			gameOverPanel.GetComponent<CanvasRenderer> ().SetAlpha (i);
			yield return new WaitForSeconds (0.01f);
		}
		gameOverPanel.GetComponent<CanvasRenderer> ().SetAlpha (1f);

		StartCoroutine(RetryButtonScript.instance.MoveIn ());
		StartCoroutine(ContinueTextScript.instance.MoveIn ());
	}
}
