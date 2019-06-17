using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISight : MonoBehaviour {

    public List<GameObject> objectInView = new List<GameObject>();
    public AISensing sensing;
    public GameObject player;

    void Awake()
    {
        sensing = GetComponentInParent<AISensing>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(objectInView.Count > 0)
        {
            foreach(GameObject go in objectInView)
            {

            }
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            sensing.bIsSeen = true;
            sensing.player = c.gameObject;
        }

        objectInView.Add(c.gameObject);
    }
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
            sensing.bIsSeen = false;

        if(objectInView.Contains(c.gameObject))
             objectInView.Remove(c.gameObject);
    }
}
