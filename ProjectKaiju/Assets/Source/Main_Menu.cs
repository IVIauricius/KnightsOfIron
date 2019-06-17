using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour {

    public Button StartGame;
    public Button Continue;
    public Button Controls;
    public Button Credits;
    public Button Exit;



	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }
}
