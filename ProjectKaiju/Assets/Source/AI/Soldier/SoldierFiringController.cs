using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFiringController : MonoBehaviour {
    public WeaponBasic myWeapon;
    public float fireTimer = 0.0f;
    public float fireRate = 2.0f;
    public float fireRange = 25.0f;
    public GameObject mechDadSees;
    public List<GameObject> playerTroops;
    public SoldierController mySoldierController;
    public Animator myAnimator;
    public GameObject MechDadSees
    {
        get
        {
            return mechDadSees;
        }
        set
        {
            mechDadSees = value;
            //GetEnemySoldiers();
            if(mechDadSees != null)
            {
                // Clear my list and add the current enemy I'm looking at's troops.
                //playerTroops.Clear();
                // Check if the lists aren't empty.
                //if(mechDadSees.GetComponent<SolSpawnF1>().myLeftSoldiers.Count == 0)
                //GetEnemySoldiers();
            }
        }
    }

    void Awake()
    {
        // If I don't have a weapon assigned by designer, try to find one attached to me.
        if (myWeapon == null)
        {
            myWeapon = GetComponent<WeaponBasic>();
        }
        // null the object I'm looking at.
        mechDadSees = null;
        // Get the animator.
        myAnimator = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(mySoldierController.stayForward);
        FiringController();
	}
    void FiringController()
    {
        // Update the timer.
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
        // Check if timer is up.
        //if (fireTimer >= fireRate && mechDadSees != null && mySoldierController.stayForward == true)
        if (fireTimer >= fireRate)
        {
            // If I'm in forward position.
            if (mySoldierController.stayForward == true)

            {
                // If we have an enemy.
                if (mechDadSees != null)
                {
                    // If close enough to enemy.
                    if (DistanceToEnemyCheck())
                    {
                        // Now fire.
                        FireAtEnemy();
                    }
                }
            }
        }
    }
    void FireMyWeapon()
    {
        Debug.Log("Firing Weapon");
        myWeapon.Fire();
    }
    void LookAtEnemy()
    {

        // Look at the enemy mech. Looks 5 units above the ground. 
        Vector3 mechPosition = new Vector3(mechDadSees.transform.position.x,
                                           mechDadSees.transform.position.y + 5,
                                           mechDadSees.transform.position.z);
        myWeapon.transform.LookAt(mechPosition);
        
    }
    bool DistanceToEnemyCheck()
    {
        // Calculate the distance between the mech and my parent.
        float distance = Vector3.Distance(mechDadSees.transform.position, transform.position);
        // Test against my firing range.
        if (distance < fireRange)
        {
            return true;
        }
        else
            return false;
    }
    void FireAtEnemy()
    {
        LookAtEnemy();      // Look at my enemy.
        FireMyWeapon();     // Fire weapon.
        fireTimer = 0.0f;   // Reset timer.
    }
    void GetEnemySoldiers()
    {
        foreach(GameObject g in mechDadSees.GetComponentInChildren<SolSpawnF1>().myLeftSoldiers)
        {
            playerTroops.Add(g);
        }
        foreach (GameObject g in mechDadSees.GetComponentInChildren<SolSpawnF1>().myRightSoldiers)
        {
            playerTroops.Add(g);
        }
    }
    void FiringAnimation()
    {
        myAnimator.SetBool("Firing", true);
    }
}
