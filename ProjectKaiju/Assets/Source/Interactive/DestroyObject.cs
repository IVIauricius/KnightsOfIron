using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public float Delay = 1.0f;

    void Start()
    {
        Destroy(gameObject, Delay);
    }
}
