using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
    public void ContinuePressed()
    {
        StartCoroutine(ContinueGame());
    }

    public void StartPressed()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator ContinueGame()
    {
         anim.Play("Open",0);

        do
        {
            yield return null;
        } while (!anim.GetCurrentAnimatorStateInfo(0).IsName("New State"));

        ScnManager.singleton.Continue();
    }

    IEnumerator StartGame()
    {
        anim.Play("Open",0);

        do
        {
            yield return null;
        } while (!anim.GetCurrentAnimatorStateInfo(0).IsName("New State"));
        ScnManager.singleton.sceneIndex = 0;
        ScnManager.singleton.AdvanceScene();
    }

    public void exit()
    {
        Application.Quit();
    }

    public void creditsPressed()
    {
        ScnManager.singleton.loadCredits();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
