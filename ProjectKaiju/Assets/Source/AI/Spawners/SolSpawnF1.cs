using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I make new soldiers, set them up, and I am parent to them.

public class SolSpawnF1 : MonoBehaviour
{
    public GameObject soldierType;
    public Formation1 myFormation1Positions;
    public int amountOfLeftSoldiers;
    public int amountOfRightSoldiers;
    public List<GameObject> myLeftSoldiers;
    public List<GameObject> myRightSoldiers;
    public TroopIconLeftMove left, right;
    private GameObject mechDadSees;
    public AudioClip[] TroopsRForwardSFX;
    public AudioClip[] TroopsRRearSFX;
    public AudioClip[] TroopsLForwardSFX;
    public AudioClip[] TroopsLRearSFX;
    public AudioSource myAudioSource;
    public GameObject MechDadSees
    {
        get { return mechDadSees; }
        set // When someone tries to set the mech we are looking at, we set all the soldiers target, too.
        {
            mechDadSees = value;
            // Go through all the soldiers and set their new target.
            foreach (GameObject g in myLeftSoldiers)
            {
                g.GetComponent<SoldierFiringController>().MechDadSees = mechDadSees;
            }
            foreach (GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierFiringController>().MechDadSees = mechDadSees;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //if (mySoldierPositions == null) { mySoldierPositions = GetComponentInParent<Transform>().gameObject.GetComponentInChildren<SoldierPositions>(); }
        SetupSoldiers();
    }

    // Update is called once per frame
    void Update()
    {
        // GetInput();
    }
    void CreateSoldier(bool side, int position)
    {
        // Create the new soldier and get a quick reference to it.
        GameObject currentSpawn = GameObject.Instantiate(soldierType, transform.position, transform.rotation);
        // Set myself as parent. 
        currentSpawn.transform.SetParent(transform);
        if (side == false)
        {
            currentSpawn.GetComponent<SoldierController>().forward = myFormation1Positions.leftForwardPositions[position].transform;
            currentSpawn.GetComponent<SoldierController>().rear = myFormation1Positions.leftRear.transform;
            myLeftSoldiers.Add(currentSpawn);
            currentSpawn.transform.position = myFormation1Positions.leftRear.transform.position;
            currentSpawn.GetComponent<SoldierController>().stayForward = true;
        }
        else if (side == true)
        {
            currentSpawn.GetComponent<SoldierController>().forward = myFormation1Positions.rightForwardPositions[position].transform;
            currentSpawn.GetComponent<SoldierController>().rear = myFormation1Positions.rightRear.transform;
            myRightSoldiers.Add(currentSpawn);
            currentSpawn.transform.position = myFormation1Positions.rightRear.transform.position;
            currentSpawn.GetComponent<SoldierController>().stayForward = true;
        }
        // Set the look at mech.
        currentSpawn.GetComponent<SoldierFiringController>().MechDadSees = mechDadSees;
    }
    void SetupSoldiers()
    {
        // Left side soldiers.
        for (int i = 0; i < amountOfLeftSoldiers; i++) { CreateSoldier(false, i); }
        // Right side soldiers.
        for (int i = 0; i < amountOfRightSoldiers; i++) { CreateSoldier(true, i); }
    }
    void GetInput()
    {
        // If player taps left forward.
        if (Input.GetKeyDown("q"))
        {

            foreach (GameObject g in myLeftSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = true;
            }
        }
        // If player taps left rear.
        if (Input.GetKeyDown("z"))
        {
            foreach (GameObject g in myLeftSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = false;
            }
        }
        // If player taps right forward.
        if (Input.GetKeyDown("r"))
        {
            foreach (GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = true;
            }
        }
        // If player taps right rear.
        if (Input.GetKeyDown("v"))
        {
            foreach (GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = false;
            }
        }
    }
     public void LeftTroopsForward()
    {
        if(!left.isForward)
        {
            left.movingF = true;
            left.movingB = false;
            left.isForward = true;
            myAudioSource.clip = TroopsLForwardSFX[Random.Range(0, TroopsLForwardSFX.Length)];
            myAudioSource.Play();
        }
       
        foreach (GameObject g in myLeftSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = true;
        }
    }
    public void LeftTroopsBack()
    {
        if(left.isForward)
        {
            left.movingF = false;
            left.movingB = true;
            left.isForward = false;
            myAudioSource.clip = TroopsLRearSFX[Random.Range(0, TroopsLRearSFX.Length)];
            myAudioSource.Play();
        }
        

        foreach (GameObject g in myLeftSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = false;
        }
    }
    public void RightTroopsForward()
    {
        if (!right.isForward)
        {
            right.movingF = true;
            right.movingB = false;
            right.isForward = true;
            myAudioSource.clip = TroopsRForwardSFX[Random.Range(0,TroopsRForwardSFX.Length)];
            myAudioSource.Play();

        }
        foreach (GameObject g in myRightSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = true;
        }
    }
    public void RightTroopsBack()
    {
        if (right.isForward)
        {
            right.movingF = false;
            right.movingB = true;
            right.isForward = false;
            myAudioSource.clip = TroopsRRearSFX[Random.Range(0, TroopsRRearSFX.Length)];
            myAudioSource.Play();
        }
        foreach (GameObject g in myRightSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = false;
        }
    }
}
