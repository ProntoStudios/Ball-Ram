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
			//PowerUpNumber = 3;
			if (PlayerScript.instance.numCurrPowerups >= System.Enum.GetNames (typeof(PlayerScript.powerups)).Length) {
				PowerUpNumber = 0;
			} else {
				do{
					powerUpNumber = Random.Range (0, System.Enum.GetNames(typeof(PlayerScript.powerups)).Length);
				}while(PlayerScript.instance.currPowerups[PowerUpNumber] == true);
			}


			/*
			//powerup limiter, DISABLE LATER
			while (PowerUpNumber != 1 && PowerUpNumber != 3 && PowerUpNumber != 4) { 
				powerUpNumber = Random.Range (0, 6);
			}*/
            collision.gameObject.GetComponent<PlayerScript>().activatePowerUp(powerUpNumber);
            GameObject.Destroy(gameObject);

        }
    }
}
