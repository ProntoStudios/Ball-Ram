using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
	public static GameControl instance;
	public int score;
	public int numProj;
	public List<GameObject> projArr;
	public int level;
	public int numDead = 0;
	public int numDeadInRow = 0;

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


	}
	
	// Update is called once per frame
	void Update () {
		if (numDead / (level * 3) > 0) {
			level++;
		}
		if(Input.GetMouseButtonDown (0)){
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

	public void deleteProj(int arrNum){

	}

}
