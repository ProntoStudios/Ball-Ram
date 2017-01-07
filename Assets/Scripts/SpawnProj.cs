using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour {
    public int maxProjs;
    private int numProjs = 0;

	// Use this for initialization
	void Start () {
		maxProjs = 1;
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
	}
	
	void CreateObstacle()
    {
        if(numProjs < maxProjs)
        {
			Instantiate(Resources.Load<GameObject>("Prefabs/Proj2"));
			Instantiate(Resources.Load<GameObject>("Prefabs/Proj4"));
            numProjs++;
        }
    }
}
