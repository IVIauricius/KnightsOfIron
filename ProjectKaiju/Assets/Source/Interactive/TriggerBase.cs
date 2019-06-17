using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBase : MonoBehaviour, Triggerable
{
    public bool TriggerEnabled = true;
    public bool UseOnce = false;
    public bool HideTrigger = true;
    public GameObject[] Targets;
    bool Used = false;

    void Start()
    {
        if(HideTrigger)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the trigger has not been used
        if (!Used)
        {
            if (other.gameObject.tag == "Player")
            {
                if (TriggerEnabled)
                {
                    for (int i = 0; i < Targets.Length; i++)
                    {
                        Targets[i].GetComponent<Triggerable>().Trigger();
                    }
                    if (UseOnce)
                    {
                        Used = true;
                    }
                }
                
            }
        }
       
    }

    public void Trigger()
    {

    }

    public void Activate()
    {
        TriggerEnabled = true;
    }

    public void Deactivate()
    {
        TriggerEnabled = false;
    }
}
