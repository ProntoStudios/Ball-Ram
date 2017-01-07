using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour {
    public int maxProjs = 15;
    private int numProjs = 0;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CreateObstacle", 1f, 1.5f);
	}
	
	void CreateObstacle()
    {
        if(numProjs < maxProjs)
        {
			Instantiate(Resources.Load<GameObject>("Prefabs/Proj0"));
			Instantiate(Resources.Load<GameObject>("Prefabs/Proj1"));
            numProjs++;
        }
    }
}
