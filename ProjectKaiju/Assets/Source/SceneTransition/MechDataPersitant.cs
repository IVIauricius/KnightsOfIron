﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDataPersitant : MonoBehaviour {
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
