using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour {

	public float slidespeed = 2.0f;
	private Vector3 origpos;
	public bool fromLeft = true;
	public float shiftdist = 20;


	// Use this for initialization
	void Start () {
		origpos = gameObject.transform.localPosition;
		Vector3 shifted = origpos;
		if (fromLeft) {
			shifted.x -= shiftdist;
		} else {
			shifted.x += shiftdist;
		}
		gameObject.transform.localPosition = shifted;
		StartCoroutine (slideback ());
	}

	private IEnumerator slideback(){
		if (fromLeft) {
			while (gameObject.transform.localPosition.x <= origpos.x) {
				gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x + slidespeed, origpos.y, origpos.z);
				yield return new WaitForSeconds (0.01f);
			}
		} 
		else {
			while (gameObject.transform.localPosition.x >= origpos.x) {
				gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x - slidespeed, origpos.y, origpos.z);
				yield return new WaitForSeconds (0.01f);
			}
		}

	}

}
