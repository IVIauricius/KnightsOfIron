using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBasic : MonoBehaviour {
    public GameObject bulletType;
    protected Transform projectileSpawn;
    public float projectileSpeed;
    public float projectileLife;
    public bool projectileUseGravity = false;
    public float loadingTime = 10.0f;
    public float weaponDamagePower = 0.0f;
    public float currentLoadingTime;
    public bool canFire = false;
    protected bool reloadSoundPlayed = false;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioSource myAudioSource;

    void Awake()
    {
        currentLoadingTime = 0.0f;
    }

	// Use this for initialization
	void Start () {
		foreach(Transform g in GetComponentsInChildren<Transform>())
        {
            if(g.name == "ProjectileSpawnPoint")
            {
                projectileSpawn = g.transform;
            //    print("In the Start Position");
            }
        }
        // Get the sound source in the object.
        myAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
        LoadCannon();
     //   CheckInput();
	}
    void CheckInput()
    {
        if(Input.GetButtonDown("Fire") && (canFire == true) )
        {
            Fire();
        }
    }
    public virtual void Fire()
    {
        GameObject currentBullet = GameObject.Instantiate(bulletType);                  // Create
        currentBullet.GetComponent<Transform>().position = projectileSpawn.position;    // Position
        currentBullet.GetComponent<Transform>().rotation = projectileSpawn.rotation;    // Rotation
        currentBullet.GetComponent<ProjectileMauro>().speedZ = projectileSpeed;         // Speed
        currentBullet.GetComponent<ProjectileMauro>().maxLife = projectileLife;         // Life
        currentBullet.GetComponent<ProjectileMauro>().ApplySpeed(projectileSpeed);      // Apply Speed Force
        currentBullet.GetComponent<ProjectileMauro>().damagePower = weaponDamagePower;  // Apply Damage.
        if (projectileUseGravity == true)                                               // Enable Gravity
        {
            currentBullet.GetComponent<Rigidbody>().useGravity = true;
        }

        PlayFireSound();

        // Reset the firing conditions.
        currentLoadingTime = 0.0f;
        canFire = false;
    }

    protected void LoadCannon()
    {
        /*
        if (currentLoadingTime > 0.0f)              // If I can't fire, reload.
        {
            currentLoadingTime -= Time.deltaTime;   // Update the loading time.
        }

        if (currentLoadingTime < 0.0f)              // Check if enough time has passed.
        {
            canFire = true;                         // Enable firing.
            //if (reloadSoundPlayed == false)         // Check if the reload sound has already played.
                PlayReloadSound();
            currentLoadingTime = 0.0f;              // Make sure the timer doesn't go crazy below 0.0f.
            // Debug.Log("Weapon can fire.");
        }
        */

        if (currentLoadingTime < loadingTime)              // If I can't fire, reload.
        {
            currentLoadingTime += Time.deltaTime;   // Update the loading time.
        }

        if (currentLoadingTime > loadingTime)              // Check if enough time has passed.
        {
            canFire = true;                         // Enable firing.
                                                    //if (reloadSoundPlayed == false)         // Check if the reload sound has already played.
            PlayReloadSound();
            currentLoadingTime = loadingTime;              // Make sure the timer doesn't go crazy below 0.0f.
            // Debug.Log("Weapon can fire.");
        }
    }
    public float GetCurrentLoadingTime()
    {
        return currentLoadingTime;
    }

    public virtual void PlayFireSound()
    {
        myAudioSource.clip = fireSound;
        myAudioSource.Play();
    }

    void PlayReloadSound()
    {
        reloadSoundPlayed = true;
        myAudioSource.clip = reloadSound;
        myAudioSource.Play();
    }
}
