using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmBehavior : RobotPart
{
    [Header("Base Arm Stats")]
    public float attackDelay;
    public int damage;
    public float knockback;
    public bool canAttack;

    public float angleOffset = 0.0f;

    public new void Start()
    {
        base.Start();
    }

    public void MoveArm(Vector3 dir)
    {
        Vector3 res = Quaternion.Euler(0, 0, angleOffset) * dir;
        transform.right = res;
    }

    public void ZeroArm()
    {
        transform.right = Quaternion.Euler(0, 0, angleOffset) * Vector3.right;
    }

    public virtual void PressAttack(float dt)
    {
        Debug.Log(this.name + " press attack");
    }

    public virtual void ReleaseAttack(float dt)
    {
        Debug.Log(this.name + " release attack");
    }

    public override bool OnPartInteract()
    {
        return base.OnPartInteract();
    }

    public override bool OnPartPickUp(GameObject player)
    {
        bool retVal = base.OnPartPickUp(player);
        return retVal;
    }

    public override bool OnPartDrop(GameObject player)
    {
        bool retVal = base.OnPartDrop(player);
        return retVal;
    }

    public override void CopyPart(RobotPart part)
    {
        base.CopyPart(part);
    }
}
