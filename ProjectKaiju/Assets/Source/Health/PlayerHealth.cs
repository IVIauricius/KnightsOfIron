using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

   public bool dead = false;

    
    public override void Update()
    {
    
        base.Update();
        //Camera transforms on death
        if(dead)
        {

            if (Camera.main.transform.localRotation.eulerAngles.y < 180f)
            {
                Camera.main.transform.RotateAround(transform.position, transform.up, 30f * Time.deltaTime);
                Camera.main.transform.localPosition += new Vector3(0, -1f * Time.deltaTime,0);
                Camera.main.fieldOfView += 7f * Time.deltaTime;
            }
            else
            {
                ScnManager.singleton.loadGameOver();
            }
        }
    }

    public override void CheckDeath()
    {
        if (currentHealth <= 0.0f && !dead)
        {
            dead = true;
            //Insert play death animation here
           
        }
    }

}
