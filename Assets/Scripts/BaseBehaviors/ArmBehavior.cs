using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmBehavior : Interactable
{
    [Header("Base Arm Stats")]
    public float attackDelay;
    public int damage;
    public bool canAttack;
    public bool finishedAttack;

    public virtual void PressAttack(float dt)
    {
        Debug.Log(this.name + " press attack");
    }

    public virtual void ReleaseAttack(float dt)
    {
        Debug.Log(this.name + " release attack");
    }
}
