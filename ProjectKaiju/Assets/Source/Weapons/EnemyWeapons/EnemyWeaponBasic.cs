using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBasic : WeaponBasic
{
    public GameObject muzzleEffect;

    // Update is called once per frame
    public override void Update()
    {
        LoadCannon();
    }

    public bool getCanFire()
    {
        return canFire;
    }
    public override void Fire()
    {
        base.Fire();
        GameObject curMuzEff = GameObject.Instantiate(muzzleEffect, projectileSpawn.position, projectileSpawn.rotation);
    }
}
