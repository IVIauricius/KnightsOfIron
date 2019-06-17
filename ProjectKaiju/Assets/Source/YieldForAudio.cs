using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldForAudio : CustomYieldInstruction {

    AudioSource a;

    public override bool keepWaiting
    {
        get
        {
            return a.isPlaying;
        }
    }

    public YieldForAudio(AudioSource a)
    {
        this.a = a;
    }
}
