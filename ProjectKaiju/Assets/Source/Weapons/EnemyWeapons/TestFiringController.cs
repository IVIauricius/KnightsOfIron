using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFiringController : MonoBehaviour {
    Rigidbody myRb;
    // Camera
    // View Range
    float minX = -20.0f;
    float maxX = 20.0f;
    float minY = -90.0f;
    float maxY = 90.0f;
    // Mouse Sensitivity
    float sensitivityX = 1.0f;
    float sensitivityY = 1.0f;
    // Current Mouse Position
    float rotationY = 0.0f;
    float rotationX = 0.0f;

	// Use this for initialization
	void Start () {
        // Get my rigidbody.
        myRb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        MouseAiming();
	}
    void CheckInput()
    {
        if (Input.GetKeyDown("w"))
        {
            // Move forward.
            myRb.AddForce(transform.forward * 500.0f);
        }
        if (Input.GetKeyDown("s"))
        {
            // Move backward.
            myRb.AddForce(-transform.forward * 500.0f);
        }
        if (Input.GetKeyDown("a"))
        {
            // Move Left.
            myRb.AddForce(-transform.right * 500.0f);
        }
        if (Input.GetKeyDown("d"))
        {
            // Move Right.
            myRb.AddForce(transform.right * 500.0f);
        }
    }
    void MouseAiming()
    {
        rotationY += Input.GetAxis("Camera X") * sensitivityY;
        rotationX += Input.GetAxis("Camera Y") * sensitivityX;

        //rotationX = Mathf.Clamp(rotationX, minX, maxX);
        //rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}
