using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
	private Rigidbody2D rb2d;
    private Transform trans;
	private float minSpeed, maxSpeed;
	public int health;
	private bool invulnerble = false;

	// Use this for initialization
	void Start () {
		health = 1;
		rb2d = GetComponent<Rigidbody2D> ();
        trans = GetComponent<Transform>();
		minSpeed = -10;
		maxSpeed = 10;
		rb2d.velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}
	void OnCollisionEnter2D(Collision2D node){
		if (node.gameObject.tag == "Shield" && !invulnerble) {
			health--;
			if (health < 1) {
				killProj ();
			} else {
				StartCoroutine (tempInv ());
			}
			
		}
		else if (node.gameObject.name == "Player") {
			GameControl.instance.deleteProj (gameObject);
		}
	}
	private IEnumerator tempInv(){
		invulnerble = true;
		yield return new WaitForSeconds (0.5f);
		invulnerble = false;
	}
    private IEnumerator killAnimation()
    {
        while (trans.localScale.x > 0 && trans.localScale.y > 0 && trans.localScale.z > 0)
        {
            trans.localScale = new Vector3(trans.localScale.x - 0.1f, trans.localScale.y - 0.1f, trans.localScale.z - 0.1f);
            yield return new WaitForSeconds(0.01f);
            
        }
        GameControl.instance.deleteProj(gameObject);
    }

    public void killProj()
    {
        
        gameObject.tag = "ProjDead";
        rb2d.velocity = new Vector3(0, 0, 0);
        StartCoroutine(killAnimation());


    }
}
