using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotArm : ArmBehavior
{
    [Header("Single Shot Arm")]
    public Transform spawnPoint;
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PressAttack(float dt)
    {
        if (canAttack)
        {
            canAttack = false;
            Fire();
            finishedAttack = true;
        }
    }

    public override void ReleaseAttack(float dt)
    {
        if (finishedAttack)
        {
            finishedAttack = false;
            StartCoroutine(ResetFire());
        }
    }

    public void Fire()
    {
        Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
