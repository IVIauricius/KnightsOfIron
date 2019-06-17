using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// What do I do?
// I shoot a ray and provide feedback about the distance of the object the ray hits.

// Class that holds the weapon's accuracy.
[System.Serializable]
public class Accuracies
{
    public float shortRange;
    public float mediumRange;
    public float longRange;
}
[System.Serializable]
public class AccuracyPenalties
{
    public float shortRange;
    public float mediumRange;
    public float longRange;
}
[System.Serializable]
public class Damages
{
    public float shortRange;
    public float mediumRange;
    public float longRange;
}
[System.Serializable]
public class DamagePenalties
{
    public float shortRange;
    public float mediumRange;
    public float longRange;
}

public class AccuracyController : MonoBehaviour {
    bool bInAimingMode;
    public bool InAimingMode
    {
        get { return bInAimingMode; }
        set
        {
            // Check if the new value is the same as it's current value. If so, do nothing.
            if (bInAimingMode == value)
            {
                // Debug.Log("bInAimingMode = " + bInAimingMode);
                return;
            }
            // Set the actual value.
            bInAimingMode = value;
            // When turning off aiming mode, turn off the accuracy controller, too.
            if (InAimingMode == false)
            {
                // Debug.Log("InAimingMode getting set to false. ResetAccuracy();");
                ResetAccuracy();
            }
        }
    }
    public Transform placeToShootRay;   // Use this to get the position and the direction to look.
    // Data for accuracy.
    public float accuracy = 0.0f;
    public float startAccuracy = 40.0f;
    public float maxAccuracy = 100.0f;
    // Data for raycast.
    private Vector3 fwd;    // The direction I'm facing.
    RaycastHit currentRay;  // The current ray we're working with.
    // Accuracies.
    public float timer = 0.0f;
    public float timeToLoad = 1.0f;
    public Accuracies myAccuracies;
    public AccuracyPenalties myAccuracyPenalties;
    private float accuracyPenaltyTotal = 0.0f;
    // Damage data.
    public Damages myDamages;
    public DamagePenalties myDamagesPenalties;
    // UI
    public Text currentAccuracyText;
    public CameraAiming myCameraAiming;
    // Mouse Sensitivity Control
    public float overMechSensitivity = 0.25f;
    private float initialMouseSensitivityX;
    private float initialMouseSensitivityY;
    // The enemy I'm currently looking at.
    public GameObject mechISee;
    public SolSpawnF1 mySoldierSpawner;

    void Awake()
    {
        
        // Find the player object.
        // In the object, get the CameraAiming component.
        mechISee = null;
    }
    // Use this for initialization
    void Start () {
        // Get the weapon's loading time.
        timeToLoad = GetComponentInParent<WeaponBasic>().loadingTime;

        accuracy = startAccuracy;
        // Get the initial mouse sensitivites from the aiming camera.
        initialMouseSensitivityX = myCameraAiming.sensitivityX;
        initialMouseSensitivityY = myCameraAiming.sensitivityY;
	}
    // This update called much more often than regular update.
    void FixedUpdate()
    {
        // This calculates the direction to shoot the ray.
        fwd = placeToShootRay.TransformDirection(Vector3.forward);

        // Shoot ray cast.
        // Create the ray, and keep it referenced.
        if (Physics.Raycast(placeToShootRay.position, fwd, out currentRay, 200))
        {
            // print("The raycast is hitting something.");
            // Debug.Log(currentRay.distance);
        }
        // Print the name of the object that I am hitting.
        if (currentRay.transform != null)
        {
            // Debug.Log("Currently hitting object named: " + currentRay.transform.gameObject.name);
        }
        // Check if the object I'm looking at is a mech.
        if (currentRay.transform != null && currentRay.transform.gameObject.GetComponent<EnemyHealth>())
        {
            // Debug.Log("I am aiming at an enemy mech.");
            SetMechISee(currentRay.transform.gameObject);
            SetAccuracy(currentRay.distance);
            myCameraAiming.SetDOFFocusTransform(currentRay.transform.gameObject.transform);
            OverMechMouseSensitivity();
        }
        else
        {
            // Debug.Log("I am not aiming at an enemy mech.");
            mechISee = null;                            // Reset the mech that I'm looking at to null.
            myCameraAiming.SetDOFFocusTransform(null);
            ResetMouseSensitivity();
        }
        // Draw the ray I'm shooting.
        Debug.DrawRay(placeToShootRay.position, placeToShootRay.forward * currentRay.distance);
    }
	
	// Update is called once per frame
	void Update () {
        timer = GetComponentInParent<WeaponBasic>().GetCurrentLoadingTime();

        // Check Input.
        //InputTesting();

        // Check if we're in aiming mode.
        if (bInAimingMode == true)
        {
            // If we are, then start the accuracy simulation.
            UpdateAccuracy();
        }
        // Draw the current accuracy on screen.
        currentAccuracyText.text = accuracy.ToString();
	}
    // Calculate Accuracy.
    void UpdateAccuracy()
    {
        timer += Time.deltaTime / timeToLoad;
        // accuracy = maxAccuracy - accuracyPenaltyTotal;
        accuracy = Mathf.Lerp(startAccuracy, maxAccuracy, timer) - accuracyPenaltyTotal;
    }
    // Checks the distance the ray has reported, and will adjust the accuracy accordingly.
    void SetAccuracy(float distance)
    {
        if (distance <= myAccuracies.shortRange)
        {
            accuracyPenaltyTotal = myAccuracyPenalties.shortRange;
        }
        else if (distance <= myAccuracies.mediumRange)
        {
            accuracyPenaltyTotal = myAccuracyPenalties.mediumRange;
        }
        else if (distance <= myAccuracies.longRange)
        {
            accuracyPenaltyTotal = myAccuracyPenalties.longRange;
        }
    }
    void ResetAccuracy()
    {
        timer = 0.0f;                   // Reset the timer.
        accuracy = startAccuracy;       // Reset the cumulative accuracy.
        accuracyPenaltyTotal = 0.0f;    // Reset the accuracy penalty.
    }
    void OverMechMouseSensitivity()     // Reduces the sensitivity with the user-defined variable overMechSensitivity.
    {
        myCameraAiming.sensitivityX = initialMouseSensitivityX - overMechSensitivity;
        myCameraAiming.sensitivityY = initialMouseSensitivityY - overMechSensitivity;
    }
    void ResetMouseSensitivity()
    {
        myCameraAiming.sensitivityX = initialMouseSensitivityX;
        myCameraAiming.sensitivityY = initialMouseSensitivityY;
    }
    void InputTesting()
    {
        
        if(Input.GetKeyDown("e"))
        {
            Debug.Log("InputTesting in AccuracyController executed.");
            switch(InAimingMode)
            {
                case true:
                    InAimingMode = false;
                    break;
                case false:
                    InAimingMode = true;
                    break;
                default:
                    break;
            }
        }
    }
    public float GetTimer()
    {
        return timer;
    }
    void SetMechISee(GameObject whatISee)
    {
        mechISee = whatISee;
        mySoldierSpawner.MechDadSees = mechISee;
    }
}
