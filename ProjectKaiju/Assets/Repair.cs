using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : MonoBehaviour {

    public bool used = false;

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.GetComponent<PlayerHealth>())
        {
            if(!used)
            {
                if (collision.transform.GetComponent<PlayerHealth>().currentHealth + collision.transform.GetComponent<PlayerHealth>().maxHealth * 0.4f < collision.transform.GetComponent<PlayerHealth>().maxHealth)
                {
                    collision.transform.GetComponent<PlayerHealth>().currentHealth += collision.transform.GetComponent<PlayerHealth>().maxHealth * 0.4f;
                }
                else
                {
                    collision.transform.GetComponent<PlayerHealth>().currentHealth = collision.transform.GetComponent<PlayerHealth>().maxHealth;
                }
                used = true;
            }
          
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
