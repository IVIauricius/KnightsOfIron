using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : WeaponBasic {
    public List<AudioClip> myReloadSounds;

    // More reloading sounds!
    public override void PlayFireSound()
    {
        fireSound = PickRandomSound();
        myAudioSource.clip = fireSound;
        myAudioSource.Play();
    }
    AudioClip PickRandomSound()
    {
        int randomNum = Random.Range(0, myReloadSounds.Count + 1);
        switch (randomNum)
        {
            case 0:
                return myReloadSounds[randomNum];
                break;
            case 1:
                return myReloadSounds[randomNum];
                break;
            case 2:
                return myReloadSounds[randomNum];
                break;
            case 3:
                return myReloadSounds[randomNum];
                break;
            default:
                return myReloadSounds[0];
                break;
        }
    }
}
