using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE
{
    PATROL, POSITIONING, FIREING, COVERING
}

public class AIInputController : MonoBehaviour {

   
    public GameObject playerRef;
    Vector3 playerPos, dirToPlayer;
    public Transform[] patrolPoints;
    int patrolPointCounter = 0, pathCounter = 0;
    public MechMovement movement;
    public Vector3 desination;
    public List<Vector3> path = new List<Vector3>();
    public float enagementDistance = 50f, tollerance = 5f;
    public bool optimalFiring, onPath, backingUp,attacking = false,unloaded = false, bHiding = false;
    [SerializeField]
    private AI_STATE _state;
    float vision, playerDis;
    public EnemyWeaponBasic weapon;
    bool firing = false;

    public AI_STATE State
    {
        get { return _state; }

        set
        {
            if (value == _state)
                return;

            switch(value)
            {
                case AI_STATE.POSITIONING:
                    if (_state == AI_STATE.PATROL || _state == AI_STATE.COVERING || State == AI_STATE.FIREING)
                        _state = value;
                    break;

                case AI_STATE.FIREING:
                    if (_state == AI_STATE.POSITIONING)
                        _state = value;
                    break;

                case AI_STATE.COVERING:
                    if (_state == AI_STATE.FIREING)
                        _state = value;
                    break;

                case AI_STATE.PATROL:
                    _state = value;
                    break;

                default:
                    break;
            }
        }
    }

   

    void Awake()
    {
        playerRef = FindObjectOfType<PlayerInputController>().gameObject;
        weapon = GetComponentInChildren<EnemyWeaponBasic>();
    }

    // Use this for initialization
    void Start () {
        //  _bPatrolStateFlag = true;
        _state = AI_STATE.PATROL;
	}

    float timer = 0;
    // Update is called once per frame
    void Update ()
    {
        playerPos = playerRef.transform.position;
        dirToPlayer = playerPos - transform.position;
        playerDis = dirToPlayer.magnitude;
        dirToPlayer.y = transform.forward.y;
        vision = Vector3.Dot(dirToPlayer.normalized, transform.forward);

        if(weapon.getCanFire())
        {
            unloaded = false;
        }
        

        DetermineAction();

        switch(_state)
        {
            case AI_STATE.POSITIONING:
                PositioningState();
                break;

            case AI_STATE.FIREING:
                print("Fire");
                FiringState();
                break;

            case AI_STATE.COVERING:
               
                break;

            case AI_STATE.PATROL:
                PatrolStateMKTWO();
                break;

            default:
                break;
        }

	}

    private void FiringState()
    {
        
       if(movement.currentGear != Gear.Neutral)
        {
            MoveToPoint(false, transform.position + transform.forward);
            return;
        }

       if(vision < .995f && (movement.currentGear == Gear.Neutral))
        {
            float rhDot = Vector3.Dot(dirToPlayer.normalized, transform.right.normalized);
            if (rhDot < 0)
            {
                movement.requestedGear = Gear.LeftRotate;
            }
            else
            {
                movement.requestedGear = Gear.RightRotate;
            }
            
            return;
        }
       else
        {
            movement.currentGear = Gear.Neutral;
            SHOOT();
        }
       

    }

    private void SHOOT()
    {
        if(!firing)
        {
            firing = true;
            StartCoroutine(FiringSequence());

            unloaded = true;
        }
      

    }

    IEnumerator FiringSequence()
    {
        movement.anim.Play("AttackSequence", 0);

        yield return new YieldForAnimState(movement.anim, "Fire");

        weapon.Fire();

        yield return new YieldForAnimState(movement.anim, "Locomotion");
        firing = false;
    }

    bool calcPath = true;
    bool retreatToRange = false;
    int cornerToPlayer;
    private void PositioningState()
    {
        NavMeshPath pathToPlayer = new NavMeshPath();
        NavMeshHit start, end;
        NavMesh.SamplePosition(transform.position, out start, 10f, NavMesh.AllAreas);

        if (playerDis > enagementDistance)
        { 

            if (calcPath)
            {     
                    NavMesh.SamplePosition(playerPos, out end, enagementDistance, NavMesh.AllAreas);
                    NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, pathToPlayer);

                cornerToPlayer = 0;
                path.Clear();
                path.AddRange(pathToPlayer.corners);

                calcPath = false;
            }
            else
            {
                if (cornerToPlayer < path.Count)
                {
                    if(MoveToPoint(false, path[cornerToPlayer]))
                    {
                        cornerToPlayer++;
                    }        
                }

            }
        }
        else if(playerDis < enagementDistance + tollerance && playerDis > enagementDistance - tollerance)
        {
            if (vision < 0.95f)
            {
                float rhDot = Vector3.Dot(dirToPlayer.normalized, transform.right.normalized);
                if (rhDot < 0)
                {
                    movement.requestedGear = Gear.LeftRotate;
                }
                else
                {
                    movement.requestedGear = Gear.RightRotate;
                }
                return;
            }
        }
        else if(playerDis < enagementDistance - tollerance && GetComponent<EnemyHealth>().currentHealth > GetComponent<EnemyHealth>().maxHealth / 2)
        {
            if (calcPath && !retreatToRange)
            {
                retreatToRange = true;
              //  print("Calc reverse");
                Vector3 backwardTar = transform.position + (-transform.forward *10f);
                print(transform.position + " " + (-transform.forward * 5f));
                print(backwardTar);
                
                NavMesh.SamplePosition(backwardTar, out end, enagementDistance, NavMesh.AllAreas);
                if(NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, pathToPlayer))
                {
                    cornerToPlayer = 0;
                    print("Clear path");
                    path.Clear();
                    path.AddRange(pathToPlayer.corners);

                    calcPath = false;
                }
                else
                {
                    movement.requestedGear = Gear.Neutral;
                    cornerToPlayer = 0;
                    print("Clear path");
                    path.Clear();
                    calcPath = false;
                    return;
                }

               
            }
            else
            {
                if (cornerToPlayer < path.Count)
                {
                  //  print("Reverse path");
                    if (MoveToPoint(true, path[cornerToPlayer]))
                    {
                  //      print("advance corner");
                        cornerToPlayer++;
                    }
                }
                else
                {
                    calcPath = true;
                    retreatToRange = false;
                }
           
            }

        }
       
    }

    private void DetermineAction()
    {
        switch (_state)
        {
            case AI_STATE.POSITIONING:
                if((playerDis < enagementDistance + tollerance && playerDis> enagementDistance - tollerance) || GetComponent<EnemyHealth>().currentHealth < GetComponent<EnemyHealth>().maxHealth / 2)
                {
                 //   print("Good range");
                    if (!unloaded)
                    {
                        print("FirsingState");
                        State = AI_STATE.FIREING;
                        break;
                    }
                    else if (vision < 0.95f)
                    {
                        if (movement.currentGear != Gear.Neutral && movement.currentGear != Gear.LeftRotate && movement.currentGear != Gear.RightRotate)
                        {
                            print("stop to attack");
                            MoveToPoint(false, transform.position + transform.forward);
                            return;
                        }
                        print("command to rotate");
                        float rhDot = Vector3.Dot(dirToPlayer.normalized, transform.right.normalized);
                        print("forward hand dot" + vision);
                        print("right hand dot" + rhDot);
                        if (rhDot < 0)
                        {
                            movement.requestedGear = Gear.LeftRotate;
                        }
                        else 
                        {
                            movement.requestedGear = Gear.RightRotate;
                        }
                        return;
                    }
                    if (movement.currentGear != Gear.Neutral)
                    {
                        MoveToPoint(false, transform.position + dirToPlayer.normalized);
                        return;
                    }
                }
                //Add logic to stay put if in good range
                else if ((vision < 0.866f && attacking) || !(playerDis < enagementDistance + tollerance && playerDis > enagementDistance - tollerance))
                {
                 //   print("CalcPath true");
                    calcPath = true;
                    return;
                }
                else if (playerDis < enagementDistance - tollerance && !retreatToRange)
                {
                    calcPath = true;
                    return;
                }
                break;

            case AI_STATE.FIREING:
                if (unloaded)
                    State = AI_STATE.POSITIONING;
                break;

            case AI_STATE.COVERING:

                break;

            case AI_STATE.PATROL:
              if(vision >= 0.866f && playerDis <= 175)
                {
                   // print("Try raycast");
                    RaycastHit hit = new RaycastHit();
                    Vector3 start, end, dir;
                    start = transform.position;
                    start.y += 5;
                    end = playerPos;
                    end.y += 5;
                    dir = end - start;
                    Ray r = new Ray(start, dir);
                    Debug.DrawRay(start, dir);
                    if (Physics.Raycast(r, out hit, 175))
                    {
                        print(hit.transform.gameObject);
                        if (hit.transform.GetComponentInParent<PlayerInputController>())
                        {
                            attacking = true;
                            State = AI_STATE.POSITIONING;
                        }
                    }
                }

                break;

            default:
                break;
        }
    }

    //Vars for this function only
    [SerializeField]
    bool reachedPoint = true, bRotating = false;
    int pointCounter = 0;
    int currentcorner = 0;
    private void PatrolStateMKTWO()
    {
        if(reachedPoint)
        {
         //   print("Begining pathing");
            NavMeshPath pathToPoint = new NavMeshPath();
            NavMeshHit start, end;
            NavMesh.SamplePosition(transform.position, out start, 10f, NavMesh.AllAreas);
            if (!NavMesh.SamplePosition(patrolPoints[pointCounter % (patrolPoints.Length - 1)].position, out end, 10f, NavMesh.AllAreas))
                return;
            NavMesh.CalculatePath(start.position, end.position, NavMesh.AllAreas, pathToPoint);
            pointCounter++;
            currentcorner = 0;

            path.Clear();
            path.AddRange(pathToPoint.corners);

            reachedPoint = false;
        }
        else
        {
            if (currentcorner < path.Count)
            {
                if (MoveToPoint(false, path[currentcorner]))
                {
                    currentcorner++;
                }
            }
            else
            {
                reachedPoint = true;
            }
        }
    }


    //USE THIS NOW
    bool MoveToPoint(bool backwards, Vector3 point)
    {
        //Set values for calculations
        Vector3 dirToPoint = point - transform.position;
        float dis = dirToPoint.magnitude;
        dirToPoint.y = transform.forward.y;
        float dot;



        if (backwards)
        {
            dot = Vector3.Dot(dirToPoint.normalized, -transform.forward.normalized);
        //    print(dot);
        }
        else
            dot = Vector3.Dot(dirToPoint.normalized, transform.forward.normalized);



        //If Ppoint is reached
        if (dis <= 5f)
        {
            if(movement.requestedGear == Gear.Forward && movement.currentGear == Gear.Backward)
            {
                movement.requestedGear = Gear.Neutral;
                return false;
            }

        //   print("stopping");
            //Shift to neutral if possible
            if(movement.currentGear == Gear.Neutral)
            {
                bRotating = false;
                return true;
            }
            if (movement.currentGear == Gear.Forward)
            {
                movement.requestedGear = Gear.Neutral;
                bRotating = false;
                return false;
            }
            if (movement.currentGear == Gear.LeftRotate || movement.currentGear == Gear.RightRotate)
            {
                movement.requestedGear = Gear.Neutral;
                bRotating = false;
                return false;
            }
            if (movement.currentGear == Gear.Backward)
            {
                movement.requestedGear = Gear.Neutral;
                bRotating = false;
                return false;
            }
            //else position to do so
            if (movement.currentGear == Gear.ForwardTwo)
            {
                movement.requestedGear = Gear.Forward;
                return false;
            }
           
        }

        //Check for rotation
        if(dot < .85f && !bRotating)
        {
       //     print("Rotation logic");
            if(movement.currentGear == Gear.Forward)
            {
                movement.requestedGear = Gear.Neutral;
                return false;
            }
            if(movement.currentGear == Gear.ForwardTwo)
            {
                movement.requestedGear = Gear.Forward;
                return false;
            }
            if (movement.currentGear == Gear.Backward)
            {
                movement.requestedGear = Gear.Neutral;
                return false;
            }

            //use dot product of right vector to decide which way to turn
            float rhDot = Vector3.Dot(dirToPoint.normalized, transform.right.normalized);

            //decide direction
            if (backwards)
            {
             //   print("Backwards rotation");
                if (rhDot > 0)
                {
                    movement.requestedGear = Gear.LeftRotate;
                }
                else
                {
                    movement.requestedGear = Gear.RightRotate;
                }
                bRotating = true;
                return false;
            }
            else
            {
                if (rhDot < 0)
                {
                    movement.requestedGear = Gear.LeftRotate;
                }
                else
                {
                    movement.requestedGear = Gear.RightRotate;
                }
                bRotating = true;
                return false;
            }
        }
       // print(dot);
        //Stop Rotating
        if(dot >= 0.90f && bRotating)
        {
         //   print("Stop turning");
            movement.requestedGear = Gear.Neutral;
            bRotating = false;
            return false;
        }
        
        if(!bRotating)
        {
            //Linear Movement
            if (movement.currentGear == Gear.Neutral)
            {
                if (!backwards)
                {
                    movement.requestedGear = Gear.Forward;
                    return false;
                }
                else
                {
                    movement.requestedGear = Gear.Backward;
                    return false;
                }

            }
            if (movement.currentGear == Gear.Forward && _state != AI_STATE.PATROL && !backwards)
            {
                movement.requestedGear = Gear.ForwardTwo;
                return false;
            }
        }
        
        
        return false;
    }

   

    void Hide()
    {
        print("Hiding State");
        
        if(!bHiding)
        {
            GameObject[] covers = GameObject.FindGameObjectsWithTag("Cover");

            GameObject closest = covers[0];

            Vector3 coverToPlayer = closest.transform.position - -playerPos;

            //Ray cast from inverse of player position to cover
            Ray r = new Ray(-playerPos, coverToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, LayerMask.NameToLayer("Cover")))
            {
                print("cover");
            }
            else
            {
                print("Raycast failed");
            }

            NavMeshPath pathToCover = new NavMeshPath();

            NavMeshHit sHit, eHit;

            NavMesh.SamplePosition(transform.position, out sHit, 10f, NavMesh.AllAreas);
            NavMesh.SamplePosition(hit.transform.position, out eHit, 20f, NavMesh.AllAreas);

            NavMesh.CalculatePath(sHit.position, eHit.position, NavMesh.AllAreas, pathToCover);

            path.Clear();
            path.AddRange(pathToCover.corners);
            pathCounter = 0;
            bHiding = true;

            onPath = true;
        }
      
    }

}
