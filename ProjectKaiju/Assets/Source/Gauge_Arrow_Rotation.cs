using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge_Arrow_Rotation : MonoBehaviour
{
    public GameObject Ben;
    public WeaponBasic myWeaponBasic;
    private bool rotating = true;
    private float start = 0f;
    private float target = -260f;
    private float speed = 0f;

	// Use this for initialization
	void Awake ()
    {
        Ben = GameObject.Find("WeaponBasic");
        //myWeaponBasic = GetComponent<WeaponBasic>();
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Fire") && (myWeaponBasic.canFire == true))
        {
           
            speed = myWeaponBasic.GetCurrentLoadingTime() / myWeaponBasic.loadingTime;
            float reverse = Mathf.Lerp(target, start, speed);
            transform.eulerAngles = new Vector3(0, 0, reverse);
        }

        speed = myWeaponBasic.GetCurrentLoadingTime() / myWeaponBasic.loadingTime;
        float angle = Mathf.Lerp(start, target, speed);
        transform.eulerAngles = new Vector3(0, 0, angle);


    }
}
