using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyCharContTest : MonoBehaviour {
    public CharacterController myController;

	// Use this for initialization
	void Start () {
        myController = GetComponent<CharacterController>();
        myController.detectCollisions = true;
        myController.GetComponent<Collider>().isTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
