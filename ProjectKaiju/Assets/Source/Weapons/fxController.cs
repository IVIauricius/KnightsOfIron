using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What do I do?
// I control the lifetime of the special effect.
// When I go below my life time, I...

public class fxController : MonoBehaviour {
    public float lifeTime;      // The max amount of time the effect will be alive.
    private float currentLife;

	// Use this for initialization
	void Start () {
        currentLife = lifeTime; // Setting my starting time correctly.
	}
	
	// Update is called once per frame
	void Update () {
        currentLife -= Time.deltaTime;  // Updating my time alive.
        if (currentLife < 0.0f)         // Check if I'm out of time.
            Destroy(gameObject);        // Destroy myself.
	}
}
