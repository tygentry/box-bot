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
    public PlayerStats playerStats;
    public Vector2 input;

    public override bool OnPartPickUp(GameObject player)
    {
        this.player = player;
        playerRB = player.GetComponent<Rigidbody2D>();
        playerStats = player.GetComponent<PlayerStats>();
        player.GetComponent<PlayerMovement>().canMove = true;
        return base.OnPartPickUp(player);
    }

    public override bool OnPartDrop(GameObject player)
    {
        player.GetComponent<PlayerMovement>().canMove = false;
        return base.OnPartDrop(player);
    }

    public virtual void LegUpdate(Vector2 playerInput, bool spacePressed)
    {

    }

    public virtual void LegFixedUpdate()
    {

    }
}
