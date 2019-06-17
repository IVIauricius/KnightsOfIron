using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageFX : MonoBehaviour {
    public List<GameObject> myDamageEffects;    // My effects prefabs.
    public EnemyHealth myHealth;               // Reference to the PlayerHealth script.
    public float firstFX = 0.8f;
    public float secondFX = 0.6f;
    public float thirdFX = 0.4f;
    public float fourthFX = 0.2f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Run();
	}
    void Run()
    {
        // 80% Check.
        if (myHealth.currentHealth < (myHealth.maxHealth * firstFX))
        {
            // If less than 80% turn on the smoke.
            myDamageEffects[0].active = true;
        }
        else
        {
            // Else leave that shit off.
            myDamageEffects[0].active = false;
        }
        // 60% Check.
        if (myHealth.currentHealth < (myHealth.maxHealth * secondFX))
        {
            // If less than 80% turn on the smoke.
            myDamageEffects[1].active = true;
        }
        else
        {
            // Else leave that shit off.
            myDamageEffects[1].active = false;
        }
        // 40% Check.
        if (myHealth.currentHealth < (myHealth.maxHealth * thirdFX))
        {
            // If less than 80% turn on the smoke.
            myDamageEffects[2].active = true;
        }
        else
        {
            // Else leave that shit off.
            myDamageEffects[2].active = false;
        }
        // 20% Check.
        if (myHealth.currentHealth < (myHealth.maxHealth * fourthFX))
        {
            // If less than 80% turn on the smoke.
            myDamageEffects[3].active = true;
        }
        else
        {
            // Else leave that shit off.
            myDamageEffects[3].active = false;
        }
    }
}
