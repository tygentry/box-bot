using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLegs : LegBehavior
{
    public override void PressDodge(float dt)
    {
        if (canDodge)
        {
            canDodge = false;
            Dodge(Vector2.zero);
        }
    }

    public override void Dodge(Vector2 dodgeDirection)
    {
        
    }
}
