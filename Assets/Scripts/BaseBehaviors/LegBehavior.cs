using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBehavior : RobotPart
{
    public float regularSpeed;
    public float dodgeSpeed;
    public float dodgeDelay;
    public bool canDodge = true;

    public GameObject player;
    public Rigidbody2D playerRB;
    public Vector2 input;
    
    public virtual void LegUpdate(Vector2 playerInput, bool spacePressed)
    {

    }

    public virtual void LegFixedUpdate()
    {

    }
}
