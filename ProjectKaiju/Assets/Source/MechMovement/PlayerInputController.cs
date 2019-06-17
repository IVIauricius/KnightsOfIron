using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(CameraAiming))]
[RequireComponent(typeof(MechMovement))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GearBoxController))]
public class PlayerInputController : MonoBehaviour {

    public MechMovement ControledMech;
    public CameraAiming AimMode;
    public GearBoxController gearBox;
    public SolSpawnF1 soldierController;
   
    public GameObject Menu;
    public GameObject GearShift;
    public GameObject root;
    public Text locationText;
    bool paused = false;

    public AudioClip GearShiftSound;
    public AudioClip NeutralShiftSound;
    public AudioSource myAudioSource;
    public AudioSource AimModeSource;

    void Awake()
    {
        ControledMech = GetComponent<MechMovement>();
        AimMode = GetComponentInChildren<CameraAiming>();
        gearBox = GetComponent<GearBoxController>();
    }

    void PlayGearShift()
    {
        myAudioSource.clip = GearShiftSound;
        myAudioSource.Play();
    }

    void PlayAimModeSound()
    {
        AimModeSource.Play();
    }

    void PlayNeutralShift()
    {
        myAudioSource.clip = NeutralShiftSound;
        myAudioSource.Play();
    }
    // Use this for initialization

    private bool _inputInUse = false;
	// Update is called once per frame
	void Update ()
    {
        locationText.text = "Location: " + transform.position;
        if (!GetComponent<PlayerHealth>().dead)
        {
            if (!ControledMech.aimMode)
            {
                if (Input.GetAxis("LeftTroopForward") > 0 && ControledMech.currentGear == Gear.Neutral)
                {
                    soldierController.LeftTroopsForward();
                   
                   
                }
                else if (Input.GetAxis("LeftTroopForward") < 0)
                {
                    soldierController.LeftTroopsBack();
                    
                    
                }

                if (Input.GetAxis("RightTroopForward") > 0 && ControledMech.currentGear == Gear.Neutral)
                {
                    soldierController.RightTroopsForward();
                  
                    
                }
                else if (Input.GetAxis("RightTroopForward") < 0)
                {
                    soldierController.RightTroopsBack();
                 
                }

                //---------------------------------Forward Input-----------------------------------
                if (Input.GetAxis("GearForward") > 0 && !_inputInUse)
                {
             //       print("Forward input");
                    _inputInUse = true;
                    if (ControledMech.currentGear == Gear.Neutral && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "FirstGear";
                        ControledMech.requestedGear = Gear.Forward;
                        PlayGearShift();
                    }
                    else if (ControledMech.currentGear == Gear.Forward && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "FirstToSecond";
                        ControledMech.requestedGear = Gear.ForwardTwo;
                        PlayGearShift();
                    }

                    else if (ControledMech.currentGear == Gear.Backward && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "ReverseToNeutral";
                        ControledMech.requestedGear = Gear.Neutral;
                        PlayNeutralShift();
                    }
                    _inputInUse = false;
                    soldierController.RightTroopsBack();
                    soldierController.LeftTroopsBack();
                  
                }
                //---------------------------------Backward Input---------------------------------------------
                else if (Input.GetAxis("GearForward") < 0 && !_inputInUse)
                {
              //      print("Backward input");
                    _inputInUse = true;
                    if (ControledMech.currentGear == Gear.Neutral && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "Reverse";
                        ControledMech.requestedGear = Gear.Backward;
                        PlayGearShift();
                    }
                    else if (ControledMech.requestedGear == Gear.Forward && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "FirstToNeutral";
                        ControledMech.requestedGear = Gear.Neutral;
                        PlayNeutralShift();
                    }
                    else if (ControledMech.currentGear == Gear.ForwardTwo && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "SecondToFirst";
                        ControledMech.requestedGear = Gear.Forward;
                        PlayGearShift();
                    }
                    _inputInUse = false;
                    soldierController.RightTroopsBack();
                    soldierController.LeftTroopsBack();
                   
                }

                //------------------------------Left Input-----------------------------------------------
                else if (Input.GetAxis("GearRight") < 0 && !_inputInUse)
                {
                //    print("Left input");
                    _inputInUse = true;
                    if (ControledMech.currentGear == Gear.Neutral && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "LeftTurn";
                        ControledMech.requestedGear = Gear.LeftRotate;
                        PlayGearShift();
                    }
                    else if (ControledMech.currentGear == Gear.RightRotate && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "RightToNeutral";
                        ControledMech.requestedGear = Gear.Neutral;
                        PlayGearShift();
                    }
                    _inputInUse = false;
                    //soldierController.RightTroopsBack();
                    //soldierController.LeftTroopsBack();
                }

                //--------------------------------Right Input----------------------------------------------
                else if (Input.GetAxis("GearRight") > 0 && !_inputInUse)
                {
               //     print("Right input");
                    _inputInUse = true;
                    if (ControledMech.currentGear == Gear.Neutral && ControledMech.currentGear == ControledMech.requestedGear)
                    {
                        gearBox.GEAR = "RightTurn";
                        ControledMech.requestedGear = Gear.RightRotate;
                        PlayGearShift();
                    }
                    else if (ControledMech.currentGear == Gear.LeftRotate && ControledMech.currentGear == ControledMech.requestedGear)
                    {

                        gearBox.GEAR = "LeftToNeutral";
                        ControledMech.requestedGear = Gear.Neutral;
                        PlayGearShift();
                    }
                    _inputInUse = false;
                    //soldierController.RightTroopsBack();
                    //soldierController.LeftTroopsBack();
                }
                //------------------------------Shoot Mode------------------------------------------------
                else if (Input.GetButtonUp("FiringMode") && ControledMech.currentGear == Gear.Neutral && GetComponentInChildren<WeaponBasic>().canFire)
                {

                    AimMode.Active = true;
                    ControledMech.aimMode = true;
                    AimMode.GetComponent<Camera>().enabled = true;
                    PlayAimModeSound();
                    //Menu.SetActive(false);
                    GearShift.SetActive(false);
                }
                else if (Input.GetButtonUp("Pause"))
                {
                    if(!paused)
                    {
                        paused = true;
                        Time.timeScale = 0;
                    }
                    else
                    {
                        paused = false;
                        Time.timeScale = 1;
                    }
                    transform.parent.GetComponentInChildren<UI>().PauseToggle();
                }
            }
            //While in Firing Mode
            else
            {
                if (Input.GetButtonUp("FiringMode"))
                {
                    Camera.main.enabled = true;
                    AimMode.GetComponent<Camera>().enabled = false;
                    AimMode.Active = false;
                    ControledMech.aimMode = false;
                    //Menu.SetActive(true);
                    GearShift.SetActive(true);
                }
                if (Input.GetButtonUp("Fire") && (GetComponentInChildren<WeaponBasic>().canFire == true))
                {
                    GetComponentInChildren<WeaponBasic>().Fire();
                 
                }

                if (Input.GetAxis("LeftTroopForward") > 0 && ControledMech.currentGear == Gear.Neutral)
                {
                    soldierController.LeftTroopsForward();


                }
                else if (Input.GetAxis("LeftTroopForward") < 0)
                {
                    soldierController.LeftTroopsBack();


                }

                if (Input.GetAxis("RightTroopForward") > 0 && ControledMech.currentGear == Gear.Neutral)
                {
                    soldierController.RightTroopsForward();


                }
                else if (Input.GetAxis("RightTroopForward") < 0)
                {
                    soldierController.RightTroopsBack();

                }
            }

        }
        else
        {
            ControledMech.enabled = false;
            
            Camera.main.enabled = true;
            AimMode.GetComponent<Camera>().enabled = false;
            AimMode.Active = false;
            ControledMech.aimMode = false;
            GearShift.SetActive(false);
            GetComponent<Animator>().SetBool("dead", true);
        }

    }
}
