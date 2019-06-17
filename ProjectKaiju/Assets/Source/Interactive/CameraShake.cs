using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour, Triggerable {

    bool Shake = false;
    public bool Activated = true;
    public float Delay = 0.0f;
    public Transform camTransform;
    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void StartShake()
    {
        Shake = true;
    }

    public void Trigger()
    {
        Invoke("StartShake", Delay);
    }

    public void Activate()
    {
        Activated = true;
    }

    public void Deactivate()
    {
        Activated = false;
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if(Activated)
        {
            if(Shake)
            {
                if (shakeDuration > 0)
                {
                    camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                    shakeDuration -= Time.deltaTime * decreaseFactor;
                }
                else
                {
                    shakeDuration = 0f;
                    camTransform.localPosition = originalPos;
                }
            }
            
        }
        
    }
}
