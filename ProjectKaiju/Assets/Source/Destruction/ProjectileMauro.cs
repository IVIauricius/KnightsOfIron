using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMauro : MonoBehaviour {
    public float speedX;
    public float speedY;
    public float speedZ;
    public Rigidbody myRb;
    public float maxLife;
    public float currentLife;
    public float damagePower;
    public GameObject collisionEffect;

    void Awake()
    {
        myRb = GetComponent<Rigidbody>();
    }
	// Use this for initialization
	void Start () {
        // Empty
	}
	
	// Update is called once per frame
	void Update () {
        // transform.Translate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
        CheckLife();
	}
    public void CheckLife()
    {
        currentLife += Time.deltaTime;
        if (currentLife > maxLife)
        {
            Debug.Log("ProjectileMauro is out of time.");
            GameObject.Destroy(gameObject);
        }
    }
    public void EnableGravity()
    {
        myRb.useGravity = true;
    }
    public void ApplySpeed(float speed)
    {
        myRb.AddForce(transform.forward * speed);
    }
    protected void CreateEffect()
    {
        // Create the prefab.
        GameObject currentEffect = GameObject.Instantiate(collisionEffect);
        // Give it my position and rotation.
        currentEffect.GetComponent<Transform>().position = transform.position;
        currentEffect.GetComponent<Transform>().rotation = transform.rotation;
    }
}
