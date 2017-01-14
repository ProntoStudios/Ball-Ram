using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public static PlayerScript instance;
    private int powerUp;
	private Rigidbody2D rb2d;
	private CircleCollider2D playerCol;
	public int health;
	private int numShield;
	public List<GameObject> shieldArr;
	private Vector3 zAxis = new Vector3 (0,0,1);


    public int PowerUp
    {
        get { return powerUp; }
        set { powerUp = value; }
    }
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		rb2d = GetComponent<Rigidbody2D> ();
		playerCol = GetComponent<CircleCollider2D> ();

		health = 1;
		numShield = 3;
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
			GameControl.instance.PlayerDied ();
		}
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Projectile") {
			health--;
			GameControl.instance.numDeadInRow = 0;
		}
	}
}
