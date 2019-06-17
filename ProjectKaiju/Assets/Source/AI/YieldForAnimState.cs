using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldForAnimState : CustomYieldInstruction {

    Animator anim;
    string name;

    public YieldForAnimState(Animator anim, string name)
    {
        this.anim = anim;
        this.name = name;
    }

    public override bool keepWaiting
    {
        get
        {
            return !anim.GetCurrentAnimatorStateInfo(0).IsName(name);
        }
    }

  
}
