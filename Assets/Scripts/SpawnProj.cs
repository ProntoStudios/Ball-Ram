using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour
{
    public int maxProj = 30;
	private int maxProjType;

    // Use this for initialization
	void Start ()
    {
        InvokeRepeating("CreateObstacle", 1.5f, 1.5f);
	}

	void Update () {


	}
	void CreateObstacle()
	{
		if (GameControl.instance.numProj < maxProj){
			if (maxProjType < 5) {
				maxProjType = GameControl.instance.level / 3 + 1;
			}
			if (maxProjType > 5) {
				maxProjType = 5;
			}
			GameControl.instance.spawnProj (Random.Range(0,maxProjType), gameObject.transform.position);// + new Vector3(4, 0, 0));
		}
    }
}
