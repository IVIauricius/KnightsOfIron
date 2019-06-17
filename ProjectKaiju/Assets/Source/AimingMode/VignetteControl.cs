using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class VignetteControl : MonoBehaviour {
    public float finalIntensity;
    public float intensityIncreasePerSecond;
    float intensity;
    VignetteAndChromaticAberration myVignette;

    void Awake()
    {
        myVignette = GetComponent<VignetteAndChromaticAberration>();
    }

	// Use this for initialization
	void Start () {
        intensity = myVignette.intensity;
    }
	
	// Update is called once per frame
	void Update () {
        increaseVignette();
	}
    void increaseVignette()
    {
        intensity += intensityIncreasePerSecond * Time.deltaTime;
        if (intensity > finalIntensity)
            intensity = finalIntensity;
        myVignette.intensity = intensity;        
    }
}
