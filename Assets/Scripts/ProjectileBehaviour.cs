using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    public float maxSpeed = 1;
    public float minSpeed = 10;

    void Start()
    {
        // Initial Velocity
        //GetComponent<Rigidbody2D>().velocity = new Vector2(10, 6);
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
    }
}

