using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMedTurret : MonoBehaviour {

    public PlayerInputController playertRef;

    void Awake()
    {
        playertRef = FindObjectOfType<PlayerInputController>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
