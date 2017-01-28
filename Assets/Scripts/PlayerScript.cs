using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript instance;
    private int powerUp;
	private Rigidbody2D rb2d;
	public int health;
	private int numShield;
	public List<GameObject> shieldArr;
	private Vector3 zAxis = new Vector3 (0,0,1);
    public Image powUpRec;
    private MoveGame moveGame;
    public enum powerups { heal, speedUp, barrier, moreShields, nuke, cash };

    // Use this for initialization
    void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		rb2d = GetComponent<Rigidbody2D> ();
        moveGame = GetComponent<MoveGame>();

		health = 1;
		numShield = GameControl.instance.saveData.initShields;
		float shieldDist = 360f / numShield;

		for (int i = 0; i < numShield; i++) {
			Vector3 newShieldPos = transform.position;
			newShieldPos.y += 3;
			GameObject tempShield = Instantiate(Resources.Load<GameObject>("Prefabs/ShieldMain"), newShieldPos, Quaternion.Euler(0, 0, 0));
			tempShield.transform.parent = gameObject.transform;
			tempShield.name = "ShieldMain" + i.ToString ();

			tempShield.transform.RotateAround ((Vector3) gameObject.transform.position, zAxis, shieldDist*i);

			shieldArr.Add (tempShield);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(health < 1){
			rb2d.velocity = Vector2.zero;
			//StartCoroutine (delShield (3, 0.5f));
			for (int i = 0; i < 7; i++) {
				GameObject tempBlob = Instantiate (Resources.Load<GameObject> ("Prefabs/PlayerDeadBlob"), gameObject.transform.position, Quaternion.Euler (0, 0, 0));
				tempBlob.GetComponent<Rigidbody2D>().velocity = new Vector2 (UnityEngine.Random.Range(-20, 20)/10f, UnityEngine.Random.Range(-20, 20)/10f);
				int scale = UnityEngine.Random.Range (10, 30);
				tempBlob.transform.localScale = new Vector3 (scale, scale, 0);
			}

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
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Projectile") {
			health--;
			GameControl.instance.numDeadInRow = 0;
		}
	}
    //enum powerups { heal, speedUp, barrier, moreShields, nuke, cash};
    public void activatePowerUp(int powerUpNmbr)
    {
        Debug.Log(powerUpNmbr);
        switch (powerUpNmbr)
        {
            case (int)powerups.heal: //HEAL
                health++;
                break;

            case (int)powerups.speedUp: //speedUp
                StartCoroutine(speedUpForSeconds(10));
                break;

            case (int)powerups.barrier: //barrier
                //shield powerup stuff here
                break;

            case (int)powerups.moreShields:
				StartCoroutine (addShield (12-GameControl.instance.saveData.initShields, 3f/(float)(12-GameControl.instance.saveData.initShields)));
                break;

            case (int)powerups.nuke:
                nuke();
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
	}
	IEnumerator addShield(int numLeft, float totDur){
		if (numLeft <= 0) {
			yield return new WaitForSeconds (10f);
			StartCoroutine(delShield (12-GameControl.instance.saveData.initShields, 3f/(float)(12-GameControl.instance.saveData.initShields)));
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
    }

    IEnumerator barrier(int seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        moveGame.moveSpeed /= 1.5f;
    }

    void nuke()
    {
        for(int i = 0; i < GameControl.instance.projArr.Count; i++)
        {
            GameControl.instance.projArr[i].GetComponent<ProjectileScript>().killProj();
        }
    }

}
