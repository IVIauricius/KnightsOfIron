using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour, Triggerable {

    public bool Activated = true;
    public SpawnObject[] AI;
    //public GameObject[] AIType;
    //public Transform[] SpawnLocation;
    bool Used = false;

    public void Trigger()
    {
        if(Activated)
        {
            for(int i=0; i<AI.Length; i++)
            {
                GameObject Inst = Instantiate(AI[i].AIClass, AI[i].SpawnLocation.position, AI[i].SpawnLocation.rotation) as GameObject;
                Inst.GetComponentInChildren<AIInputController>().patrolPoints = AI[i].PatrolPoints;
            }
        }
    }

    public void Activate()
    {
        Activated = true;
    }

    public void Deactivate()
    {
        Activated = false;
    }



}
