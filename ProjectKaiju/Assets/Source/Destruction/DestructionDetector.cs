using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What do I do?
// I check if I have collided with the wall object.
// If so, I check what my collision radius is colliding with and 

public class DestructionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other) // Check collisions with DestructionWalls
    {
        if (other.tag == "DestructionWall")
        {
            // Call the function in ExplosionRadius that gathers all the objects it is colliding with and apply the forces.

            // Get all the wall pieces.
            Collider[] wallPieces = Physics.OverlapSphere(transform.position, 1.0f);
            // Assign forces to them.
        }
    }
}
