using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCycler : MonoBehaviour {

    public Image display;
    public Sprite[] backgrounds;
    int counter = 0;
    float timer = 0;
    public float delay = 5f;

	// Use this for initialization
	void Start () {
        display.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            display.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
            timer = 0f;
        }
	}
}
