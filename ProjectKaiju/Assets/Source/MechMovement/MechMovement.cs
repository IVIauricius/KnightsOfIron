using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Gear
{
    Neutral = 0, Forward =1, ForwardTwo = 2, Backward = 3, LeftRotate = 4, RightRotate = 5
}

//[RequireComponent(typeof (Rigidbody))]

public class MechMovement : MonoBehaviour
{
    public float gravity = 9.8f;

    public Gear currentGear
    {
        get { return _currentGear; }

        set
        {
            if (_currentGear != value)
                _currentGear = value;
        }
    }

    [SerializeField]
    private Gear _currentGear;

  //  [SerializeField]
    public Gear requestedGear
    {
        get { return _requestedGear; }

        set
        {
            if (requestedGear == value || bIShifting)
                return;

            _requestedGear = value;

            bIShifting = true;
                        
        }
    }

    [SerializeField]
    private Gear _requestedGear;

    public float throttle, turnSpeed;

    public CharacterController controller;

    [HideInInspector]
    public bool aimMode = false;
    
    private bool bIShifting;

    public Animator anim;
  
    void Awake()
    {
        anim = GetComponent<Animator>();

        controller = GetComponent<CharacterController>();
        
    }

	// Use this for initialization
	void Start () {
        controller.detectCollisions = true;
    }

   

	// Update is called once per frame
	void FixedUpdate ()
    {
      if(!aimMode)
        {
            setGear();
            Move();
            if (!controller.isGrounded)
            {
                controller.Move(new Vector3(0, -Mathf.Pow(gravity,2) * Time.deltaTime, 0));
            }
        }
      
    }



    float timer = 0f, turnTimer = 0f, animSpeed, startTurn,startingWalk =0;
    bool doOnce = true;
    private void setGear()
    {
        if(bIShifting)
        {

            switch (requestedGear)
            {
                case Gear.Neutral:

                    timer += Time.deltaTime / 0.25f;

                    turnTimer += Time.deltaTime / 0.25f;

                    float slowingSpeed = Mathf.Lerp(throttle / 2, 0, timer);
                    float slowTurn = Mathf.Lerp(turnSpeed, 0, turnTimer);

                    if (_currentGear == Gear.Forward || _currentGear == Gear.Backward)
                    {
                     
                        animSpeed = Mathf.InverseLerp(0, throttle, slowingSpeed);

                        if(currentGear == Gear.Backward)
                            anim.SetFloat("Speed", -animSpeed);
                        else
                        anim.SetFloat("Speed", animSpeed);
                    }
                    else
                    {
                        animSpeed = Mathf.InverseLerp(0, turnSpeed, slowTurn);
                        if(_currentGear == Gear.LeftRotate)
                        {
                            anim.SetFloat("Turnspeed", -animSpeed);
                        }
                        else
                            anim.SetFloat("Turnspeed", animSpeed);
                    }
                    

                   // print(slowTurn);

                    if (controller.velocity.magnitude > 0.02f)
                    {
                      if(currentGear == Gear.Forward)
                        {
                            controller.Move(transform.forward.normalized * slowingSpeed * Time.deltaTime);
                        }
                      else if(currentGear == Gear.Backward)
                        {
                            controller.Move(transform.forward.normalized * -slowingSpeed * Time.deltaTime);
                        }
                    

                    }
                    if(currentGear == Gear.LeftRotate || currentGear == Gear.RightRotate)
                    {
                        if (currentGear == Gear.LeftRotate)
                        {
                            transform.Rotate(new Vector3(0, -slowTurn * Time.deltaTime, 0));
                        }
                        else if (currentGear == Gear.RightRotate)
                        {
                            transform.Rotate(new Vector3(0, slowTurn * Time.deltaTime, 0));
                        }
                    }

                    if (timer >= 1)
                    {
                        anim.SetTrigger("Neutral");
                        //anim.Play("Idle");
                        currentGear = requestedGear;
                        bIShifting = false;
                        doOnce = true;
                        timer = 0f;
                        slowTurn = 1.0f;
                        turnTimer = 0f;
                    }
                    break;

                case Gear.Forward:


                    if(currentGear == Gear.Neutral)
                    {
                        if (doOnce)
                        {
                            anim.SetTrigger("First");
                            doOnce = false;
                        }
                      
                        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
                        

                        timer += Time.deltaTime / 1.5f;

                        float startingup = 0.1f;
                        startingup = Mathf.Lerp(startingup, throttle / 2, timer);

                        animSpeed = Mathf.InverseLerp(0, throttle / 2, startingup);
                      //  print(startingup);
                        anim.SetFloat("Speed", animSpeed);

                        controller.Move(transform.forward.normalized * startingup * Time.deltaTime);

                        if (timer >= 1)
                        {
                            doOnce = true;
                            currentGear = requestedGear;
                            bIShifting = false;
                            timer = 0;
                         //   print("shifted");
                            break;

                        }
                    }
                    else if(currentGear == Gear.ForwardTwo)
                    {
                        timer += Time.deltaTime / 0.25f;

                        float shiftingDown = Mathf.Lerp(throttle, throttle/2, timer);

                        anim.SetFloat("Speed", shiftingDown / throttle);

                        controller.Move(transform.forward.normalized * shiftingDown * Time.deltaTime);

                        if (timer >= 1)
                        {
                            
                            currentGear = requestedGear;
                            bIShifting = false;
                            timer = 0;
                         //   print("Debug in shifting from second to first");
                           // anim.SetTrigger("First");
                            break;

                        }
                    }
                    
                      

                    break;

                case Gear.ForwardTwo:

                    timer += Time.deltaTime / 0.25f;

                    float speedUp = Mathf.Lerp(throttle / 2, throttle, timer);

                    animSpeed = Mathf.InverseLerp(0, throttle, speedUp);
                    anim.SetFloat("Speed", animSpeed);

                    controller.Move(transform.forward.normalized * speedUp * Time.deltaTime);

                    if (timer >= 1)
                    {
                        
                        currentGear = requestedGear;
                        bIShifting = false;
                        anim.SetTrigger("Second");
                        timer = 0;
                        break;
                        
                    }
                    break;

                   case Gear.Backward:


                    if (currentGear == Gear.Neutral)
                    {
                        anim.SetTrigger("Reverse");

                        timer += Time.deltaTime / 0.25f;

                        float backingUp = Mathf.Lerp(0, throttle / 2, timer);

                        controller.Move(transform.forward.normalized * -backingUp * Time.deltaTime);

                        animSpeed = Mathf.InverseLerp(backingUp,-throttle /2 , timer);

                        anim.SetFloat("Speed", -animSpeed);

                        if (timer >= 1)
                        {
                            
                            currentGear = requestedGear;
                            bIShifting = false;

                            timer = 0;
                            break;

                        }

                    }
            
                    break;

                case Gear.LeftRotate:
                    //  anim.SetTrigger("Left");
                    turnTimer += Time.deltaTime / 0.25f;


                    startTurn = Mathf.Lerp(0, -turnSpeed, turnTimer);

                    animSpeed = Mathf.InverseLerp(0, -turnSpeed, startTurn);
                    anim.SetFloat("Turnspeed", -animSpeed);

                    transform.Rotate(new Vector3(0, startTurn * Time.deltaTime, 0));

                    if (turnTimer >= 1)
                    {
                        currentGear = requestedGear;
                        turnTimer = 0;
                        bIShifting = false;
                        break;
                    }

                    break;
                case Gear.RightRotate:
                    //anim.SetTrigger("Right");
                    turnTimer += Time.deltaTime / 0.25f;


                    startTurn = Mathf.Lerp(0, turnSpeed, turnTimer);

                    animSpeed = Mathf.InverseLerp(0, turnSpeed, startTurn);
                    anim.SetFloat("Turnspeed", animSpeed);

                    transform.Rotate(new Vector3(0, startTurn * Time.deltaTime, 0));

                    if (turnTimer >= 1)
                    {
                        currentGear = requestedGear;
                        turnTimer = 0;
                        bIShifting = false;
                        break;
                    }

                    break;
            }
        }
    }

    public void Move()
    {
        if (!bIShifting)
        {

            

            switch (currentGear)
            {
                case Gear.Neutral:

                    break;
                case Gear.Forward:
                    controller.Move(transform.forward.normalized * (throttle / 2) * Time.deltaTime);
                    anim.SetFloat("Speed", 0.5f);
                    if (controller.velocity.magnitude > throttle)
                    {
                      //  rb.velocity = rb.velocity.normalized * (throttle / 2);
                    }

                    break;

                case Gear.ForwardTwo:
                    controller.Move(transform.forward.normalized * throttle * Time.deltaTime);
                    anim.SetFloat("Speed", 1f);
                    break;

                case Gear.Backward:
                    controller.Move(transform.forward.normalized * -(throttle / 2) * Time.deltaTime);
                    break;

                case Gear.LeftRotate:
                    transform.Rotate(new Vector3(0, -turnSpeed * Time.deltaTime, 0));

                    
                    break;

                case Gear.RightRotate:
                    transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0));

                    
                    break;
            }
        }
    }
}
