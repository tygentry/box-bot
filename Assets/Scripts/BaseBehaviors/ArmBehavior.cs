using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmBehavior : Interactable
{
    [Header("Base Arm Stats")]
    public float attackDelay;
    public int damage;

    public virtual void HoldAttack(float dt)
    {
        Debug.Log(this.name + " hold attack");
    }

    public virtual void ReleaseAttack(float dt)
    {
        Debug.Log(this.name + " release attack");
    }
}
