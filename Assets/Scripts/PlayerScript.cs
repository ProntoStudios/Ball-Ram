using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript instance;
    private int powerUp;
	private Rigidbody2D rb2d;
	public int health;
	public bool alive = true;
	private int numShield;
	public List<GameObject> shieldArr;
	public bool weakShields;
	private Vector3 zAxis = new Vector3 (0,0,1);
    public Image powUpRec;
    private MoveGame moveGame;

	public enum powerups { speedUp, heal, barrier, moreShields, nuke, cash };
	public bool[] currPowerups = new bool[System.Enum.GetNames(typeof(powerups)).Length];
	public int numCurrPowerups = 0;

	public List<GameObject> deadBlobArr;

    // Use this for initialization
    void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		rb2d = GetComponent<Rigidbody2D> ();
        moveGame = GetComponent<MoveGame>();

		if (GameControl.instance.saveData.character == "weak12") {
			weakShields = true;
		} else {
			weakShields = false;
		}
		health = 1;
		numShield = GameControl.instance.initShields;
		float shieldDist = 360f / numShield;

		for (int i = 0; i < numShield; i++) {
			Vector3 newShieldPos = transform.position;
			newShieldPos.y += 3;
			GameObject tempShield = Instantiate(Resources.Load<GameObject>("Prefabs/ShieldMain"), newShieldPos, Quaternion.Euler(0, 0, 0));
			tempShield.transform.parent = gameObject.transform;
			tempShield.name = "ShieldMain" + i.ToString ();

			tempShield.transform.RotateAround ((Vector3) gameObject.transform.position, zAxis, shieldDist*i);
			tempShield.transform.localScale = new Vector3 (0, tempShield.transform.localScale.y, tempShield.transform.localScale.z);
			StartCoroutine(tempShield.GetComponent<ShieldMainScript> ().enable ());
			shieldArr.Add (tempShield);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(health < 1 && alive){
			alive = false;
			rb2d.velocity = Vector2.zero;
			//StartCoroutine (delShield (3, 0.5f));
			for (int i = 0; i < 7; i++) {
				GameObject tempBlob = Instantiate (Resources.Load<GameObject> ("Prefabs/PlayerDeadBlob"), gameObject.transform.position, Quaternion.Euler (0, 0, 0));
				tempBlob.GetComponent<Rigidbody2D>().velocity = new Vector2 (UnityEngine.Random.Range(-20, 20)/10f, UnityEngine.Random.Range(-20,20)/10f);
				float scale = UnityEngine.Random.Range (0.8f, 1.2f);
				tempBlob.transform.localScale = new Vector3 (scale, scale, 0);
				deadBlobArr.Add (tempBlob);
			}
			StartCoroutine (shieldEndAnim());
			MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
			foreach(MonoBehaviour script in scripts)
			{
				script.enabled = false;
			}
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			gameObject.GetComponent<CircleCollider2D>().enabled = false;

			GameControl.instance.PlayerDied ();
		}
	}
	IEnumerator shieldEndAnim(){
		while (GameControl.instance.rotateSpeed > 0.5f) {
			GameControl.instance.rotateSpeed /= 1.1f;
			yield return new WaitForSeconds (0.02f);
		}
		for (int i = 0; i < shieldArr.Count; i++) {
			StartCoroutine(shieldArr [i].GetComponent<ShieldMainScript> ().disable ());
		}
		/*
		if (GameControl.instance.saveData.character == "weak12") {
			for (int i = 0; i < shieldArr.Count; i++) {
				StartCoroutine(shieldArr [i].GetComponent<ShieldMainScript> ().disableFor (300f));
			}
		} else {
			
			while (shieldArr [0].transform.localScale.x > 0.05f) {
				GameControl.instance.rotateSpeed /= 1.1f;
				for (int i = 0; i < shieldArr.Count; i++) {
					shieldArr [i].transform.localScale = new Vector3 (shieldArr [i].transform.localScale.x - 0.05f, shieldArr [i].transform.localScale.y, shieldArr [i].transform.localScale.z);
				}
				yield return new WaitForSeconds (0.015f);
			}
			for (int i = 0; i < shieldArr.Count; i++) {
				shieldArr [i].transform.localScale = new Vector3 (0, 0, 0);
			}

		}*/
	}
	public void Unkill(){
		GameControl.instance.Continue ();
		health = 1;
		alive = true;
		MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
		foreach(MonoBehaviour script in scripts)
		{
			script.enabled = true;
		}
		PlayerScript.instance.moveGame.enabled = false;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		//blobs
		int numFrames = 60;
		StartCoroutine(Unkill_Blobs(numFrames));
		StartCoroutine(Unkill_Player(numFrames));
		//shiuelds
		gameObject.GetComponent<CircleCollider2D>().enabled = true;
		//player movegame script is enabled inside the unkill_blob script
	}
	private IEnumerator Unkill_Blobs(int numFrames){
		List<Vector2> deadShift = new List<Vector2>();
		for (int i = 0; i < 7; i++) {
			Vector2 temp = PlayerScript.instance.transform.position - deadBlobArr [i].transform.position;
			temp.Scale(new Vector2(1f/(float)numFrames, 1f/(float)numFrames));
			deadShift.Add(temp);
		}
		for(int i =0; i <numFrames; i++){
			for(int j = 0; j < 7; j++){
				deadBlobArr [j].transform.position = (Vector2)deadBlobArr [j].transform.position + (Vector2)deadShift[j];
			}
			yield return new WaitForSeconds (0.01f);
		}
		int initMax = deadBlobArr.Count;
		for(int i = 0; i < initMax; i++){
			GameObject.Destroy (deadBlobArr [deadBlobArr.Count-1]);
			deadBlobArr.Remove (deadBlobArr [deadBlobArr.Count-1]);
		}
		PlayerScript.instance.moveGame.enabled = true;
	}
	private IEnumerator Unkill_Player(int numFrames){
		for(int i = 1; i <= numFrames; i++){
			gameObject.transform.localScale = new Vector3 (1.9f*(float)i/(float)numFrames,1.9f*(float)i/(float)numFrames,0.1f);
			yield return new WaitForSeconds (0.01f);
		}
		GameControl.instance.rotateSpeed = 2.5f;
		for (int i = 0; i < shieldArr.Count; i++) {
			StartCoroutine(shieldArr [i].GetComponent<ShieldMainScript> ().enable ());
		}
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Projectile") {
			health--;
			GameControl.instance.numDeadInRow = 0;
		}
	}
    //enum powerups { heal, speedUp, barrier, moreShields, nuke, cash};
    public void activatePowerUp(int powerUpNmbr)
    {
		numCurrPowerups++;
		currPowerups [powerUpNmbr] = true;
        switch (powerUpNmbr)
		{

			case (int)powerups.speedUp: //speedUp
				StartCoroutine(speedUpForSeconds(5));
				break;

            case (int)powerups.heal: //HEAL
                break;

            case (int)powerups.barrier: //barrier
                //shield powerup stuff here
                break;

			case (int)powerups.moreShields:
				if (GameControl.instance.saveData.character == "default") {
					StartCoroutine (addShield (12 - shieldArr.Count, 3f / (float)(12 - GameControl.instance.initShields)));
				}
                break;

			case (int)powerups.nuke:
				StartCoroutine (nuke ());
                break;

			case (int)powerups.cash:
				StartCoroutine(boostCoinOdds(10f));
				break;
        }
    }
	IEnumerator boostCoinOdds(float seconds){
		int temp = GameControl.instance.coinSpawnOdds;
		GameControl.instance.coinSpawnOdds = 1;
		yield return new WaitForSeconds (seconds);
		GameControl.instance.coinSpawnOdds = temp;
		numCurrPowerups--;
		currPowerups [(int)powerups.cash] = false;
	}
	IEnumerator addShield(int numLeft, float totDur){
		if (numLeft <= 0) {
			yield return new WaitForSeconds (5f);
			StartCoroutine(delShield (12-GameControl.instance.initShields, 3f/(float)(12-GameControl.instance.initShields)));
		} else {
			numLeft--;
			int numFrames = (int)(totDur * 60);

			numShield++;
			float shieldShift = (360f / (numShield - 1)) - (360f / numShield);
			float deltaShift = shieldShift / (float)numFrames;


			GameObject tempShield = Instantiate (Resources.Load<GameObject> ("Prefabs/ShieldMain"), shieldArr [0].transform.position, shieldArr [0].transform.rotation);
			tempShield.transform.parent = gameObject.transform;
			tempShield.name = "ShieldMain" + shieldArr.Count.ToString ();
			shieldArr.Add (tempShield);

			for (int i = 0; i < numFrames; i++) {
				for (int j = (shieldArr.Count - 1); j >= 0; j--) {
					shieldArr [j].transform.RotateAround ((Vector3)gameObject.transform.position, zAxis, deltaShift * (shieldArr.Count - 1 - j));
				}
				yield return new WaitForSeconds (totDur / (float)numFrames);
			}

			StartCoroutine(addShield (numLeft, totDur));
		}
	}
	IEnumerator delShield(int numLeft, float totDur){ 
		if (numLeft <= 0) {
			numCurrPowerups--;
			currPowerups [(int)powerups.moreShields] = false;
		} else {
			numLeft--;
			int numFrames = (int)(totDur * 60);

			numShield--;
			float shieldShift = (360f / (numShield)) - (360f / (numShield+1));
			float deltaShift = shieldShift / (float)numFrames;

			for (int i = 0; i < numFrames; i++) {
				for (int j = (shieldArr.Count - 1); j >= 0; j--) {
					shieldArr [j].transform.RotateAround ((Vector3)gameObject.transform.position, zAxis, deltaShift * j);
				}
				yield return new WaitForSeconds (totDur / (float)numFrames);
			}

			GameControl.Destroy(shieldArr [shieldArr.Count - 1]);
			shieldArr.RemoveAt (shieldArr.Count - 1);
			StartCoroutine(delShield (numLeft, totDur));
		}
	}/*
	IEnumerator shiftShield(int numLeft, float dis, float totDur){
		for (int i = 0; i < shieldArr.Count; i++) {
			shieldArr[i].transform.position = 
		}
	}*/
    IEnumerator speedUpForSeconds(int seconds)
    {
        moveGame.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(seconds);
        moveGame.moveSpeed /= 1.5f;
		numCurrPowerups--;
		currPowerups [(int)powerups.speedUp] = false;
    }


	public IEnumerator nuke()
    {
        for(int i = 0; i < GameControl.instance.projArr.Count; i++)
        {
            GameControl.instance.projArr[i].GetComponent<ProjectileScript>().killProj();
		}

		yield return new WaitForSeconds(10);
		numCurrPowerups--;
		currPowerups [(int)powerups.nuke] = false;
    }

}
