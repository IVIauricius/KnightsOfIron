using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour, Triggerable {

    public Sprite face;
    public TextAsset text;
    public AudioSource source;
    public float Delay = 0.0f;
    public GameObject NextLine;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayDialogue()
    {
        SubtitleController.singleton.StartSubtitles(face, text.text, source);
        NextLine.GetComponent<Triggerable>().Trigger();
    }

    public void Trigger()
    {
        Invoke("PlayDialogue", Delay);
    }

    public void Activate()
    {
        throw new NotImplementedException();
    }

    public void Deactivate()
    {
        throw new NotImplementedException();
    }
}
