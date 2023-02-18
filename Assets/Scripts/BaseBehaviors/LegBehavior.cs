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

    public override bool OnPartPickUp(GameObject player)
    {
        bool retVal = base.OnPartPickUp(player);
        this.player = player;
        playerRB = player.GetComponent<Rigidbody2D>();
        player.GetComponent<PlayerMovement>().canMove = true;
        return retVal;
    }

    public override bool OnPartDrop(GameObject player)
    {
        bool retVal = base.OnPartDrop(player);
        player.GetComponent<PlayerMovement>().canMove = false;
        return retVal;
    }

    public virtual void LegUpdate(Vector2 playerInput, bool spacePressed)
    {

    }

    public virtual void LegFixedUpdate()
    {

    }
}
