using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour, Triggerable {

    AudioClip Sound;
    public float Volume = 1.0f;
    public bool Enabled = true;
    public GameObject[] SpawnVFX;
    public float Delay = 1.0f;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Sound = audioSource.clip;
    }

    // Play Sound
    public void Trigger()
    {
        if(Enabled)
        {
            audioSource.PlayOneShot(Sound, Volume);
            Invoke("Spawn", Delay);
        }
    }

    void Spawn()
    {
        for(int i = 0; i < SpawnVFX.Length; i++)
        {
            GameObject inst = Instantiate(SpawnVFX[i], this.transform.position, this.transform.rotation) as GameObject;
        }
    }
    
    // Activate
    public void Activate()
    {
        Enabled = true;
    }

    // Deactivate
    public void Deactivate()
    {
        Enabled = false;
    }
}
