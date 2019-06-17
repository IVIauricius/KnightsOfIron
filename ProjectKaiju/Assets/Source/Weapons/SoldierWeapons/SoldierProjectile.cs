using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierProjectile : ProjectileMauro {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckLife();
	}
    void OnCollisionEnter(Collision other)
    {
        // Detect if the object I'm colliding with has enemy health.
        //Checks in parent because of new collider added for raycast purposes
        if (other.gameObject.GetComponentInParent<EnemyHealth>())
        {
            // I've hit something with a PlayerHealth script. Kill it!
            //Debug.Log("Detected Collision with PlayerMech.");
            // Reduce health of mech.
            //Debug.Log("Deducting Health from PlayerHealth.");
            other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(damagePower);
        }

        // Create the fx prefab.
        CreateEffect();

        // Destroy myself when I collide with ANYTHING.
        //Debug.Log("Destroying EnemyProjectile.");
        Destroy(gameObject);
    }
}
