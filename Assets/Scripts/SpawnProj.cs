using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour
{
    public int maxProj = 100;

    // Use this for initialization
	void Start ()
    {
        InvokeRepeating("CreateObstacle", 1.5f, 1.5f);
	}
	
	void CreateObstacle()
    {
		if(GameControl.instance.numProj < maxProj)
		{
			GameControl.instance.spawnProj (Random.Range(0,5), gameObject.transform.position);
        }
    }
}
