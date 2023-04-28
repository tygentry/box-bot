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
        proj.transform.right = transform.right;
        StartCoroutine(ResetFire());
    }

    IEnumerator ResetFire()
    {
        float t1 = Time.time;
        yield return new WaitForSeconds(attackDelay * (1.0f / stats.GetStat(PlayerStats.ModifiableStats.AttackSpeed)));
        print(Time.time - t1);
        canAttack = true;
    }
}
