using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : Health {

    public GameObject damage, spawnpoint;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        GameObject spawned = GameObject.Instantiate(damage, spawnpoint.transform);
        spawned.GetComponentInChildren<TextMeshPro>().SetText(damageAmount.ToString());
        spawned.GetComponentInChildren<DamageBillboard>().started = true;
        GetComponent<AIInputController>().State = AI_STATE.POSITIONING;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
	}
}
