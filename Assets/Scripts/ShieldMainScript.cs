using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMainScript : MonoBehaviour {

	private Vector3 zAxis = new Vector3 (0,0,1);
	public bool weakShields;
	private bool isDisabled = false;
	private SpriteRenderer spRd;

	// Use this for initialization
	void Start () {
		GameControl.instance.rotateSpeed = 2.5f;
		spRd = gameObject.GetComponent<SpriteRenderer> ();
		if (GameControl.instance.saveData.character == "weak12") {
			weakShields = true;
		} else {
			weakShields = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*
		if (GameControl.instance.rotateSpeed < 3f) {
			GameControl.instance.rotateSpeed *= 1.01f;
		}
		*/
		var playerPos = GameObject.Find ("Player").transform.position;
		gameObject.transform.RotateAround ((Vector3) playerPos, zAxis, GameControl.instance.rotateSpeed);
		//gameObject.transform.localScale = (Vector3)gameObject.transform.localScale + new Vector3 (0.1f,0,0);

	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Projectile") {
			if (weakShields) {
				StartCoroutine (disableFor (12f));
			} else {
				StartCoroutine(ColorHit ());
			}
		}
	}
	public IEnumerator ColorHit(){
		for(float i = 0.6f; i <= 1f; i+= 0.01f){
			spRd.color = new Color (i, i, 1f);
			yield return new WaitForSeconds (0.03f);
		}
		spRd.color = new Color (1f, 1f, 1f);
	}
	public Vector3 toPlayerNorm (){
		return (PlayerScript.instance.transform.position - gameObject.transform.position).normalized;
	}
	public IEnumerator disableFor(float seconds){
		if (!isDisabled) {
			isDisabled = true;
			float currScale = gameObject.transform.localScale.x;
			while (gameObject.transform.localScale.x > 0.05f) {
				gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x - 0.07f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				yield return new WaitForSeconds (0.015f);
			}
			gameObject.transform.localScale = new Vector3 (0, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			yield return new WaitForSeconds (seconds);
			if(PlayerScript.instance.health > 0){
				while (gameObject.transform.localScale.x < currScale) {
					gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + 0.07f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
					yield return new WaitForSeconds (0.015f);
				}
				gameObject.transform.localScale = new Vector3 (currScale, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			}
			isDisabled = false;
		}
	}
}
