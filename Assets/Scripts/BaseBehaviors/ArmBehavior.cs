using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmBehavior : RobotPart
{
    [Header("Base Arm Stats")]
    public float attackDelay;
    public int damage;
    public bool canAttack;
    public Transform aimTransform;

    public new void Start()
    {
        base.Start();
        aimTransform = FindObjectOfType<AimTracker>().transform;
    }

    public virtual void PressAttack(float dt)
    {
        Debug.Log(this.name + " press attack");
    }

    public virtual void ReleaseAttack(float dt)
    {
        Debug.Log(this.name + " release attack");
    }

    public override bool OnPartPickup()
    {
        return base.OnPartPickup();
    }
}
