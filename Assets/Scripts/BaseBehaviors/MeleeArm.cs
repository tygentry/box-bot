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

    new void Start()
    {
        base.Start();
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
        print(stats.GetStat(PlayerStats.ModifiableStats.AttackSpeed));
        float t1 = Time.time;
        yield return new WaitForSeconds((attackDelay + activeHitTime) * (1.0f / stats.GetStat(PlayerStats.ModifiableStats.AttackSpeed)));
        print(Time.time - t1);
        canAttack = true;
    }

    public void Hit(Collider2D collision)
    {

    }
}
