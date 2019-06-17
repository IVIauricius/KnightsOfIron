using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHealth : Health {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        CheckDeath();
	}
    public override void CheckDeath()
    {
        if (currentHealth <= 0.0f)
        {
            GetComponentInParent<SoldierController>().DestroySelf();
        }
    }
}
