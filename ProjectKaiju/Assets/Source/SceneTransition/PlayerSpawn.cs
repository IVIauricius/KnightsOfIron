using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {

    public float health = 500;
    public int numSoldiersL = 5;
    public int numSoldiersR = 5;
    public GameObject playerMech;
    public bool StartFresh;
    public static PlayerSpawn singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
    public void SpawnPlayer()
    {

        if (ScnManager.singleton.save != null || !StartFresh)
        {
            health = ScnManager.singleton.save.Health;
            numSoldiersR = ScnManager.singleton.save.rightNumSol;
            numSoldiersL = ScnManager.singleton.save.leftNumSol;
        }

        playerMech.GetComponentInChildren<PlayerHealth>().currentHealth = health;
        playerMech.GetComponentInChildren<SolSpawnF1>().amountOfRightSoldiers = numSoldiersR;
        playerMech.GetComponentInChildren<SolSpawnF1>().amountOfLeftSoldiers = numSoldiersL;

        GameObject go = GameObject.Instantiate(playerMech,transform.position,transform.rotation);
      
    }

	// Update is called once per frame
	void Update () {
		
	}
}
