using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour {
    public int maxProjs;
    //public 
    private int numProjs = 0;
    public GameObject prefab = Resources.Load<GameObject>("Prefabs/Spawner");

    // Use this for initialization
    void Start () {
		maxProjs = 100;
        InvokeRepeating("CreateObstacle", 1.5f, 1.5f);
	}
	
	void CreateObstacle()
    {
        if(numProjs < maxProjs)
		{
            GameObject proj = Resources.Load<GameObject>("Prefabs/Proj" + Random.Range(0, 5));
            Vector3 position = prefab.transform.position;
            Debug.Log(position.x + ", " + position.y + ", " + position.z);
            Instantiate(proj, prefab.transform.position, Quaternion.Euler(0, 0, 0));
            numProjs++;
        }
    }
}
