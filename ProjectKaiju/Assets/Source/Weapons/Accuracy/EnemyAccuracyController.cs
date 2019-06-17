using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What do I do? 
// I keep track of what the player is looking at.
// Reduce my accuracy depending on what I'm looking at.

public class EnemyAccuracyController : MonoBehaviour {
    public AIInputController myController;
    public SolSpawnF1 mySoldierSpawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(mySoldierSpawner.MechDadSees)
        {
            if (mySoldierSpawner.MechDadSees != myController.playerRef)
            {
                SetTroopsTarget();
            }
        }

		
	}
    void SetTroopsTarget()
    {
        mySoldierSpawner.MechDadSees = myController.playerRef;
    }
}
