using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBoxController : MonoBehaviour {

    public Animator anim;

    private string _gear;
    public string GEAR
    {
        get { return _gear; }

        set
        {
            if (value == _gear)
                return;

            else
            {
                _gear = value;
                anim.Play(_gear);
            }
        }
    }

	
}
