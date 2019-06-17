using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFX : MonoBehaviour {
    public float life = 1.0f;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= life)
        {
            Destroy(gameObject);
        }
	}
}
