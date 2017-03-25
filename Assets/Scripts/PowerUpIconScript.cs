using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIconScript : MonoBehaviour {
	private int slotPos = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public IEnumerator activate(float timeOn){
		float flashTime = 0f;
		if (timeOn > 2f) {
			flashTime = 2f;
			timeOn = timeOn - flashTime;
		} else {
			flashTime = timeOn;
			timeOn = 0;
		}
		GetComponent<CanvasRenderer> ().SetAlpha (1);
		yield return new WaitForSeconds (timeOn);

		float deltaFade = flashTime / 50f;
		for(float i = flashTime; i >0f; i = i - deltaFade){
			yield return new WaitForSeconds (deltaFade);
			gameObject.GetComponent<CanvasRenderer> ().SetAlpha (i/flashTime);
		}
		PlayerScript.instance.removePowIconArr (slotPos);
	}

	public void setSlotPosition(int slot){
		int deltaSlot = slot - slotPos;
		slotPos = slot;
		gameObject.transform.localPosition = new Vector2 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - deltaSlot*70);
	}
}
