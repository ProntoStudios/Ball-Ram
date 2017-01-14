using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
	private int value;
	private Transform trans;
	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		value = Random.Range (1, 10);
		StartCoroutine(killClock ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator killClock(){
		yield return new WaitForSeconds(5f);
		StartCoroutine(killAnimation());
	}
	void OnTriggerEnter2D (Collider2D node){
		if (node.name == "Player") {
			GameControl.instance.coinTot += value;
			delete();
			Debug.Log ("Coin: " + GameControl.instance.coinTot.ToString ());
		}
	}
	private IEnumerator killAnimation()
	{
		while (trans.localScale.x > 0 && trans.localScale.y > 0 && trans.localScale.z > 0)
		{
			trans.localScale = new Vector3(trans.localScale.x - 0.1f, trans.localScale.y - 0.1f, trans.localScale.z - 0.1f);
			yield return new WaitForSeconds(0.02f);

		}
		delete();
	}
	void delete(){
		if (gameObject == null) {
			return;
		} else {
			GameObject.Destroy (gameObject);
		}
	}
}
