using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour
{
    public GameObject prefab;
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
            GameObject proj = Resources.Load<GameObject>("Prefabs/Proj" + Random.Range(0, 5));
            Vector3 position = prefab.transform.position;
            GameControl.instance.spawnProj (Random.Range(0,5), position);
        }
    }
}
