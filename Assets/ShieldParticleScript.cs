using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldParticleScript : MonoBehaviour {
	private Transform trans;
	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform>();
		StartCoroutine (killAnimation ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private IEnumerator killAnimation()
	{
		while (trans.localScale.x > 0.03f && trans.localScale.y > 0.03f && trans.localScale.z > 0.03f)
		{
			trans.localScale = new Vector3(trans.localScale.x/1.1f, trans.localScale.y/1.1f, trans.localScale.z/1.1f);
			yield return new WaitForSeconds(0.03f);

		}
		GameObject.Destroy (gameObject);
	}
}
