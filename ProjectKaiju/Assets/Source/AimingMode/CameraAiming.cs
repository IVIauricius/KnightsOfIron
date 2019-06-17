using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[System.Serializable]
public class VignetteProperties
{
    public float startingIntensity;                     // Where the vignette begins.
    public float finalIntensity;                        // The intensity we should end at.
    public VignetteAndChromaticAberration myVignette;   // Reference to the vignette in the object.
}
[System.Serializable]
public class FOVProperties
{
    public float startingFOV;   //
    public float finalFOV;      //
    public Camera myCamera;     // The camera I'm affecting.
}
[System.Serializable]
public class DOFProperties
{
    public float startingDistance;      //
    public float finalDistance;         //
    public DepthOfField myDepthOfField; // The script I'm affecting.
}

public class CameraAiming : MonoBehaviour {

    public bool Active
    {
        get { return activated; }

        set
        {
            if(activated == value)
            {
                return;
            }
            activated = value;

            if (activated)
                GetComponent<Camera>().enabled = true;
            else
            {
                GetComponent<Camera>().enabled = false;
                Reset();
            }
        }
    }

    public bool activated = false;
    // View range.
    public float minX = -20f;
    public float maxX = 20f;
    public float minY = -90f;
    public float maxY = 90f;
    // Mouse sensitivity.
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;
    // Keeps track of the current mouse positions.
    float rotationY = 0f;
    float rotationX = 0f;
    // Camera control properties.
    public float timeToFinalAccuracy;
    public VignetteProperties myVignetteProperties;
    public FOVProperties myFieldOfViewProperties;
    public DOFProperties myDepthOfFieldProperties;
    float timer = 0.0f;
    public SpriteRenderer cameraBar;
    // Accuracy Controller.
    public AccuracyController myAccuracyController;

    void Awake()
    {
        // Get the vignette.
        if (myVignetteProperties.myVignette == null) { myVignetteProperties.myVignette = GetComponent<VignetteAndChromaticAberration>(); }
        // Set the vignette to the desired intensity.
        myVignetteProperties.myVignette.intensity = myVignetteProperties.startingIntensity;
        // Get the camera.
        if (myFieldOfViewProperties.myCamera == null) { myFieldOfViewProperties.myCamera = GetComponent<Camera>(); }
        if (myDepthOfFieldProperties.myDepthOfField == null) { myDepthOfFieldProperties.myDepthOfField = GetComponent<DepthOfField>(); }
        // Get my accuracy controller and set it's time.
        if (myAccuracyController == null)
        {
            myAccuracyController = GetComponentInChildren<AccuracyController>();
            if (myAccuracyController != null)
            {
                myAccuracyController.timeToLoad = timeToFinalAccuracy;
            }
        }
        
    }

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;           // Hides the mouse.
	}
	
	// Update is called once per frame
	void Update () {
        if(activated)
        {
            Run();
        }
	}
    void Run()
    {
        CheckEscape();
        MouseAiming();
        AccuracyControl();
        cameraBar.enabled = true;
    }
    void Reset()
    {
        myVignetteProperties.myVignette.intensity = myVignetteProperties.startingIntensity;
        myFieldOfViewProperties.myCamera.fieldOfView = myFieldOfViewProperties.startingFOV;
        timer = 0.0f;
        cameraBar.enabled = false;
    }
    void CheckEscape() // To unlock the mouse, reveal it, and be able to use it.
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void MouseAiming()
    {
        rotationY += Input.GetAxis("Camera X") * sensitivityY;
        rotationX += Input.GetAxis("Camera Y") * sensitivityX;

        rotationX = Mathf.Clamp(rotationX, minX, maxX);
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
    void AccuracyControl()
    {
        // Controls the properties with the time we want.
        timer += Time.deltaTime / timeToFinalAccuracy;
        // Vignette.
        myVignetteProperties.myVignette.intensity = Mathf.Lerp(myVignetteProperties.startingIntensity, myVignetteProperties.finalIntensity, timer);
        // Field of View.
        myFieldOfViewProperties.myCamera.fieldOfView = Mathf.Lerp(myFieldOfViewProperties.startingFOV, myFieldOfViewProperties.finalFOV, timer);
        // Depth of Field.
        myDepthOfFieldProperties.myDepthOfField.focalLength = Mathf.Lerp(myDepthOfFieldProperties.startingDistance, myDepthOfFieldProperties.finalDistance, timer);
    }
    public void SetDOFFocusTransform(Transform focusObject)
    {
        myDepthOfFieldProperties.myDepthOfField.focalTransform = focusObject;
    }
}
