using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeArm : ArmBehavior
{
    [Header("Melee Arm")]
    public GameObject hitboxObj;
    private HitBox hitbox;
    public float activeHitTime;

    void Start()
    {
        print("test");
        hitbox = hitboxObj.GetComponent<HitBox>();
    }

    public override void PressAttack(float dt)
    {
        if (canAttack)
        {
            canAttack = false;
            Swing();
        }
    }

    public override void ReleaseAttack(float dt)
    {
        
    }

    public void Swing()
    {
        hitbox.Activate(activeHitTime);
        StartCoroutine(ResetSwing());
    }

    IEnumerator ResetSwing()
    {
        yield return new WaitForSeconds(attackDelay+activeHitTime);
        canAttack = true;
    }

    public void Hit(Collider2D collision)
    {

    }
}
