using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour {

	GameObject proj0;
	GameObject proj1;
	public int spawnrate = 200;
	private int counter = 0;
	private bool foundPrefabs = false;

	// Use this for initialization
	void Start () {
		proj0 = Resources.Load<GameObject> ("Prefabs/proj0");
		proj1 = Resources.Load<GameObject> ("Prefabs/proj1");
		foundPrefabs = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter++;

		if (counter >= spawnrate&&foundPrefabs) {
			Vector3 pos = gameObject.transform.localPosition;
			Instantiate (proj0, pos, Quaternion.identity);
			Instantiate (proj1, pos, Quaternion.identity);
			counter = 0;
		}
	}
}
