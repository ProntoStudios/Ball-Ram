using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour {
    public int maxProjs;
    private int numProjs = 0;

	// Use this for initialization
	void Start () {
		maxProjs = 100;
        InvokeRepeating("CreateObstacle", 1.5f, 1.5f);
	}
	
	void CreateObstacle()
    {
        if(numProjs < maxProjs)
		{
			Instantiate(Resources.Load<GameObject>("Prefabs/Proj" + Random.Range(0,5)));
            numProjs++;
        }
    }
}
