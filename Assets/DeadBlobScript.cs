using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBlobScript : MonoBehaviour {
	private Transform trans;
	private Rigidbody2D rb2d;
	private CircleCollider2D cc2d;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		rb2d = GetComponent<Rigidbody2D> ();
		cc2d = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Shield") {
			cc2d.enabled = false;
			StartCoroutine (killAnimation ());
		}
	}
	private IEnumerator killAnimation()
	{
		rb2d.velocity = new Vector3(0, 0, 0);
		while (trans.localScale.x > 0.1f && trans.localScale.y > 0.1f)
		{
			trans.localScale = new Vector3(trans.localScale.x / 1.1f, trans.localScale.y / 1.1f, trans.localScale.z);
			yield return new WaitForSeconds(0.02f);

		}
		GameControl.instance.deleteProj(gameObject);
	}
}
