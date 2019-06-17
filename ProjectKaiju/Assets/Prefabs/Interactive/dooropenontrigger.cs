using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooropenontrigger : MonoBehaviour , Triggerable {
    public Animator Anim;

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }

    public void Trigger()
    {
        Anim.Play("door");
    }

    // Use this for initialization var FallerObject : GameObject; //Implement your Faller game object into this variable in the inspector


    void Start () {
   

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
