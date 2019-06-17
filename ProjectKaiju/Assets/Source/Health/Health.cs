using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;
    public GameObject Explosion;

    // Use this for initialization
    public virtual void Start () {
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        CheckDeath();
	}
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
    //Robert- Created virtual function for PlayerHealth to override
    public virtual void CheckDeath()
    {

        if (currentHealth <= 0.0f)
        {
            GameObject clone;
            clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(transform.parent.gameObject);
        }   
    }
}
