using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : ProjectileMauro {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckLife();
	}
    void OnCollisionEnter(Collision other)
    {
        // Check for EnemyMech.
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            // I've hit something with an EnemyHealth script. Kill it!
            //Debug.Log("Detected Collision with EnemyMech.");
            // Reduce health of mech.
            //Debug.Log("Deducting Health from EnemyHealth.");
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damagePower);
        }
        // Check for Soldier.
        //if(other.gameObject.GetComponent<SoldierHealth>())
        //{
        //    other.gameObject.GetComponent<SoldierHealth>().TakeDamage(damagePower);
        //}

        // Create the fx prefab.
        CreateEffect();

        // Destroy myself when I collide with ANYTHING.
        Debug.Log("Destroying PlayerProjectile.");
        Destroy(gameObject);
    }
}
