using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBehavior : Interactable
{
    public float regularSpeed;
    public float dodgeSpeed;
    public float dodgeDelay;
    public bool canDodge = true;
    public virtual void PressDodge(float dt)
    {
        Debug.Log(this.name + " press dodge");
    }

    public virtual void ReleaseDodge(float dt)
    {
        Debug.Log(this.name + " release dodge");
    }

    public virtual void Dodge(Vector2 dodgeDirection)
    {
        Debug.Log(this.name + " dodge");
    }

    public virtual void LegUpdate(Vector2 input)
    {

    }

    public virtual void LegFixedUpdate()
    {

    }
}
