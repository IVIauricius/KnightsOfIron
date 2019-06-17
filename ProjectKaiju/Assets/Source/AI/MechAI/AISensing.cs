using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AISensing : MonoBehaviour {

    private bool _bIsSeen, _bIsHeard, _bIsBlocked;
    public AIInputController ControlledAI;
    public Image enemySight;
    public AISight eyes;
    public AIHearing ears;
    public GameObject player;

    public bool bIsBlocked
    {
        get { return _bIsBlocked; }

        set
        {
            if (value == _bIsBlocked)
                return;

         //   print("bIsBlocked Update");

            _bIsSeen = value;

            //ControlledAI. Call new state here 
        }
    }

    public bool bIsSeen
    {
        get { return _bIsSeen; }

        set
        {
            if (value == _bIsSeen)
                return;

         //   print("bIsSeen Update");

            _bIsSeen = value;

          //  ControlledAI.Posturing = _bIsSeen;
        }
    }

    public bool bIsHeard
    {
        get { return bIsHeard; }

        set
        {
            if (value == bIsHeard)
                return;

            bIsHeard = value;

           // print("bIsHeard Update");

            //ControlledAI. Call new state here 
        }
    }

    void Awake()
    {
        eyes = GetComponentInChildren<AISight>();
        ears = GetComponentInChildren<AIHearing>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //TEMP DISABLED IN ORDER TO TEST
        //if (bIsSeen)
        //    enemySight.enabled = false;
        //else
        //    enemySight.enabled = true;
	}
}
