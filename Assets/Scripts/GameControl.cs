using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static GameControl instance;
	public int score;
	public int numProj;
	public int numPow;
	public List<GameObject> projArr;
	public int level;
	public int numDead = 0;
	public int numDeadInRow = 0;
	public float rotateSpeed = 3f;



	public List<GameObject> powArr;
	private float minX, maxX, minY, maxY;
	public int nmbrOfPowerUps;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		score = 0;
		numProj = 0;
		level = 1;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.targetFrameRate = 60;


	}

	// Update is called once per frame
	void Update () {
		if (numDead / (level * 3) > 0) {
			level++;
		}
		if(Input.touchCount == 3){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); //restarts game
		}
	}

	private void OnGUI(){
		GUI.Label (new Rect (10, 10, 100, 30), "Level: " + level);
		GUI.Label (new Rect (10, 40, 100, 30), "Score: " + score);
		GUI.Label (new Rect (10, 70, 100, 30), "Health: " + PlayerScript.instance.health);

	}


	public void PlayerDied(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); //restarts game
	}

	public void spawnProj(int projType, Vector3 pos){
		GameObject tempProj = Instantiate(Resources.Load<GameObject>("Prefabs/Proj" + projType.ToString()), pos, Quaternion.Euler(0, 0, 0));
		tempProj.name = "proj" + projType.ToString () + "_" + numProj.ToString ();
		projArr.Add (tempProj);
		numProj++;
	}

	public void deleteProj(GameObject other){
		numDead++;
		numDeadInRow++;
		score += numDeadInRow;
		numProj--;

		projArr.Remove (other);
		GameControl.Destroy (other);
	}

	public void spawnPowerUp(int type)
	{
		GameObject tempPwrUp = Instantiate(Resources.Load<GameObject>("Prefabs/PowerUp"));
		tempPwrUp.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
		tempPwrUp.GetComponent<powerUp>().PowerUpNumber = Random.Range(0, nmbrOfPowerUps-1);
		powArr.Add(tempPwrUp);
		numPow++;

	}

}