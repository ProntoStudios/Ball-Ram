using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMoveScript : MonoBehaviour {
	public static ScoreMoveScript instance;
	private float moveDist;
	public Text EndScoreText;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		moveDist = (float)GameOverPanelScript.instance.gameObject.GetComponent<RectTransform>().rect.height/8f + 50f;

	}
	public IEnumerator MoveIn(){
		EndScoreText = GetComponent<Text> ();
		EndScoreText.text = "Score: " + GameControl.instance.score;
		float speed = -20f;
		float accel = ((speed*speed - (9))/(2f*moveDist));
		while (speed < 3f) {
			gameObject.transform.localPosition = new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + speed);
			speed += accel;
			yield return new WaitForSeconds (0.005f);
		}
	}

	public IEnumerator MoveOut(){
		float speed = 20f;
		float accel = -((speed*speed - (9))/(2f*moveDist));
		while (speed > -3f) {
			gameObject.transform.localPosition = new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + speed);
			speed += accel;
			yield return new WaitForSeconds (0.005f);
		}
	}
}
