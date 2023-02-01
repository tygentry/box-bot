using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLegs : LegBehavior
{
    public float dodgeDuration = 0.5f;
    private float dodgeTimer;
    private float dodgeCooldownTimer;
    private bool dodging = false;
    private Vector2 dodgeDirection;

    public override void LegUpdate(Vector2 playerInput, bool spacePressed)
    {

        dodgeTimer -= Time.deltaTime;
        dodgeCooldownTimer -= Time.deltaTime;
        input = playerInput;

        if(dodgeTimer <= 0 && dodging)
        {
            dodging = false;
            canDodge = false;
            dodgeCooldownTimer = dodgeDelay;
        }
        if(dodgeCooldownTimer <= 0 && !canDodge)
        {
            canDodge = true;
        }

        if(!dodging && canDodge && spacePressed)
        {
            dodging = true;
            dodgeDirection = input;
            dodgeTimer = dodgeDuration;
        }
    }

    public override void LegFixedUpdate()
    {
        if(dodging)
        {
            playerRB.MovePosition((Vector2)player.transform.position + dodgeSpeed * Time.deltaTime * dodgeDirection.normalized);
        } 
        else
        {
            playerRB.MovePosition((Vector2)player.transform.position + regularSpeed * Time.deltaTime * input.normalized);
        }
    }
}
