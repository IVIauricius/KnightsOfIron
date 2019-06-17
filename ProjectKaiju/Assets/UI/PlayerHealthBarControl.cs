using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarControl : MonoBehaviour {

    public PlayerHealth health;
    public Slider healthBar;
    public Text ones, tens, hund;

	// Use this for initialization
	void Start () {
        healthBar.maxValue = health.maxHealth;
       
    }
	
	// Update is called once per frame
	void Update () {
        healthBar.value = health.currentHealth;
        int one = ((int)health.currentHealth) % 10;
        int ten = ((int)(health.currentHealth % 100)) / 10;
        int hundred = ((int)health.currentHealth) / 100;

        ones.text = one.ToString();
        tens.text = ten.ToString();
        hund.text = hundred.ToString();
    }
}
