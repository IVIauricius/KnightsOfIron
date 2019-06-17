using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I make new soldiers, set them up, and I am parent to them.

public class SoldierSpawner : MonoBehaviour {
    public GameObject soldierType;
    public SoldierPositions mySoldierPositions;
    public int amountOfLeftSoldiers;
    public int amountOfRightSoldiers;
    public List<GameObject> myLeftSoldiers;
    public List<GameObject> myRightSoldiers;

	// Use this for initialization
	void Start () {
        //if (mySoldierPositions == null) { mySoldierPositions = GetComponentInParent<Transform>().gameObject.GetComponentInChildren<SoldierPositions>(); }
        SetupSoldiers();
	}
	
	// Update is called once per frame
	void Update () {
      //  GetInput();
	}
    void CreateSoldier(bool side)
    {
        // Create the new soldier and get a quick reference to it.
        GameObject currentSpawn = GameObject.Instantiate(soldierType);
        // Set myself as parent. 
        currentSpawn.transform.SetParent(transform);
        if (side == false)
        {
            currentSpawn.GetComponent<SoldierController>().forward = mySoldierPositions.leftForward.transform;
            currentSpawn.GetComponent<SoldierController>().rear = mySoldierPositions.leftRear.transform;
            myLeftSoldiers.Add(currentSpawn);
        }
        else if (side == true)
        {
            currentSpawn.GetComponent<SoldierController>().forward = mySoldierPositions.rightForward.transform;
            currentSpawn.GetComponent<SoldierController>().rear = mySoldierPositions.rightRear.transform;
            myRightSoldiers.Add(currentSpawn);
        }
    }
    public void SetupSoldiers()
    {
        // Left side soldiers.
        for(int i = 0; i < amountOfLeftSoldiers; i++) { CreateSoldier(false); }
        // Right side soldiers.
        for(int i = 0; i < amountOfRightSoldiers; i++) { CreateSoldier(true); }
    }

    public void RightTroopsForward()
    {
         foreach(GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = true;
            }
    }

    public void RightTroopBack()
    {
        foreach (GameObject g in myRightSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = false;
        }
    }

    public void LeftTroopsForward()
    {
        foreach (GameObject g in myLeftSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = true;
        }
    }

    public void LeftTroopBack()
    {
        foreach (GameObject g in myLeftSoldiers)
        {
            g.GetComponent<SoldierController>().stayForward = false;
        }
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
            foreach(GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = true;
            }
        }
        // If player taps right rear.
        if (Input.GetKeyDown("v"))
        {
            foreach(GameObject g in myRightSoldiers)
            {
                g.GetComponent<SoldierController>().stayForward = false;
            }
        }
    }
}
