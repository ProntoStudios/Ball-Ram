using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScoreHealth : MonoBehaviour
{
    public Text text;
    private PlayerScript player;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.Find("Player").GetComponents<PlayerScript>()[0];
        
        //player = GetComponent<PlayerScript>();
        //text.text = "Hello There!";
    }
	
	// Update is called once per frame
	void Update ()
    {
        player = GameObject.Find("Player").GetComponents<PlayerScript>()[0];
        text.text = "Health: " + player.health;
	}
}
