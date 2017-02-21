using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {
	private Transform trans;
	private Rigidbody2D rb2d;
	public int coinValMin = 1;
	public int coinValMax = 2;
	private bool deleted = false;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		rb2d = GetComponent<Rigidbody2D> ();
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
			delete ();
			//PlayerScript.instance.activatePowerUp (Random.Range (0, 6));
		} else if (node.tag == "Wall") {
			rb2d.velocity = Vector2.zero;
		}
	}
	private IEnumerator killAnimation()
	{
		while (trans.localScale.x > 0 && trans.localScale.y > 0 && trans.localScale.z > 0)
		{
			trans.localScale = new Vector3(trans.localScale.x - 0.1f, trans.localScale.y - 0.1f, trans.localScale.z - 0.1f);
			yield return new WaitForSeconds(0.02f);

		}
		if (gameObject != null){
			GameObject.Destroy (gameObject);
		}
	}
	void delete(){
		if (gameObject == null) {
			return;
		} else {
			if (!deleted) {
				GameObject.Destroy (gameObject);
				GameControl.instance.coinTot++;
				//Debug.Log ("Coin: " + GameControl.instance.coinTot.ToString ());
				deleted = true;
			}
		}
	}
}
