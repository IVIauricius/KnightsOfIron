using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTansition : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        print("Collision");
        if(collider.GetComponent<PlayerInputController>())
        {
            print("Player");
            ScnManager.singleton.AdvanceScene();
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
