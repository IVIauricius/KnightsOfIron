using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLighting : MonoBehaviour, Triggerable {

    public Light obj;
    public bool enabledL = true;
    float time = 720;
    public float rotX;
    public float rotY;
    public float rotZ;
    public float delay = 0.1f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Trigger()
    {
        if (enabledL)
        {
            Debug.Log("rotating");
            StartCoroutine(rotate());
        }
    }

    //this function should work (probably, I think) but I can't seem to get it working with the trigger cause Josh doesn't like commenting to explain how his code works - 4/30/18
    //I figured it out cause programming is fun and intuitive - 5/1/18
    IEnumerator rotate ()
    {
        Debug.Log("rotating");
        //get change per second for each axis
        float changeX = rotX / time;
        float changeY = rotY / time;
        float changeZ = rotZ / time;
        for (int i = 0; i < time; i++)
        {
            //increment light rotation
            obj.transform.Rotate(changeX, changeY, changeZ);
            yield return new WaitForSeconds(delay);
        }
    }
    public void Activate()
    {
        enabledL = true;
    }
    public void Deactivate()
    {
        enabledL = false;
    }
}
