using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour {

    private int powerUpNumber;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public int PowerUpNumber
    {
        get { return powerUpNumber; }
        set { powerUpNumber = value; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {

            collision.gameObject.GetComponent<PlayerScript>().PowerUp = powerUpNumber;
            GameObject.Destroy(gameObject);
        }
    }
}
