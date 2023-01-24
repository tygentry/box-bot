using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotArm : ArmBehavior
{
    [Header("Single Shot Arm")]
    public Transform spawnPoint;
    public GameObject projectile;

    public override void PressAttack(float dt)
    {
        if (canAttack)
        {
            canAttack = false;
            Fire();
        }
    }

    public override void ReleaseAttack(float dt)
    {
        
    }

    public void Fire()
    {
        GameObject proj = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        proj.transform.right = aimTransform.position - spawnPoint.position;
        StartCoroutine(ResetFire());
    }

    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
