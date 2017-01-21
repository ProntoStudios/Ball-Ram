using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProj : MonoBehaviour
{
    public int maxProj = 30;
	private int maxProjType;
	private float pauseTime = 1.3f;

    public float spawnXOffset = 0.0f;
    public float spawnYOffset = 0.0f;

    // Use this for initialization
    void Start ()
    {
		StartCoroutine(ProjSpawner());
	}

	void Update () {
		

	}
	void CreateObstacle()
	{
		maxProj = GameControl.instance.level + 5;
		if (GameControl.instance.numProj < maxProj){
			if (maxProjType < 5) {
				maxProjType = GameControl.instance.level / 3 + 1;
			}
			if (maxProjType > 5) {
				maxProjType = 5;
			}

            Vector3 spawnPosition = gameObject.transform.position + new Vector3(spawnXOffset, spawnYOffset);
			GameControl.instance.spawnProj (Random.Range(0,maxProjType), spawnPosition);// + new Vector3(4, 0, 0));
		}
    }
	IEnumerator ProjSpawner()
	{
		while (true) {
			yield return new WaitForSeconds (pauseTime);
			CreateObstacle ();
		}
	}
}
