using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour, Triggerable {

    public bool Activated = true;
    public GameObject Player;
    public float Delay = 1.0f;
	
    public void Trigger()
    {
        Player.GetComponent<Camera>().enabled = false;
        GetComponent<Camera>().enabled = true;
        Invoke("SwitchCam", Delay);
    }

    void SwitchCam()
    {
        Player.GetComponent<Camera>().enabled = true;
        GetComponent<Camera>().enabled = false;
    }

    public void Activate()
    {

    }
    
    public void Deactivate()
    {

    }
}
