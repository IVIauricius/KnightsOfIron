using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopIconRightMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        print(transform.position);
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("Hit Up!");
            transform.position = new Vector3(300, 570, 0);
            print(transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            print("Hit Down!");
            transform.position = new Vector3(300, 510, 0);
            print(transform.position);
        }
    }
}

