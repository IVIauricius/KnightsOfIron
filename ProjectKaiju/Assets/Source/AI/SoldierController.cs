using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// I keep the soldier following the mech.

public class SoldierController : MonoBehaviour {
    public Transform forward;   // The forward position I need to follow.
    public Transform rear;      // The rear position I need to follow.
    Transform positionToFollow; // The position I need to update to THIS frame/update.
    public bool stayForward;    // Toggles whether I'm forward or backward.
    NavMeshAgent myNavAgent;    // The NavMeshAgent attached to me.
    public Animator myAnimator; // The soldier's animator component.
    float animBlendSpeed;       // The speed the animation is blending at.

    void Awake()
    {
        if (myNavAgent == null) { myNavAgent = GetComponent<NavMeshAgent>(); }
        if(myAnimator == null)
        {
            myAnimator = GetComponentInChildren<Animator>();
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StayWithPosition();
        Animator();
	}
    void StayWithPosition()
    {
        // Check if I'm in forward or rear position.
        if (stayForward)
        {
            positionToFollow = forward;
        }
        else
        {
            positionToFollow = rear;
        }
        // Moves me toward the positions.
        myNavAgent.destination = positionToFollow.position;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public void Animator()
    {
        // I run every frame.
        CheckVelocity();
    }
    public void CheckVelocity()
    {
        // Print the value of my velocity.
        // Debug.Log(myNavAgent.velocity);

        // I check if the velocity is at a certain point and make decisions.
        if(myNavAgent.velocity != Vector3.zero)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
    }
}
