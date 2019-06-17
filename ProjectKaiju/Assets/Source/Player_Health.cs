using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    public Slider HealthBar;
    public int Health = 100;

    private int currentHealth;

	// Use this for initialization
	void Start ()
    {
        currentHealth = Health;
	}

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar.value = currentHealth;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Health == 100)
        {
            TakeDamage(1);
        }
	}
}
