using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour {

    
    private int powerUpNumber;
	// Use this for initialization
	void Start () {
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
			powerUpNumber = Random.Range (0, 6);
            collision.gameObject.GetComponent<PlayerScript>().activatePowerUp(powerUpNumber);
            GameObject.Destroy(gameObject);

        }
    }
}
