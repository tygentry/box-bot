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
    public Transform aimTransform;

    public int followMouse = -1;
    public float angleOffset = 0.0f;

    public new void Start()
    {
        base.Start();
        aimTransform = FindObjectOfType<AimTracker>().transform;
    }

    public void Update()
    {
        if (followMouse == 0)
        {
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = Quaternion.Euler(0, 0, angleOffset) * (Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y));
        }
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
        followMouse = 0;
        return retVal;
    }

    public override bool OnPartDrop(GameObject player)
    {
        bool retVal = base.OnPartDrop(player);
        followMouse = -1;
        return retVal;
    }

    public override void CopyPart(RobotPart part)
    {
        base.CopyPart(part);
        //this.angleOffset = part.GetComponent<ArmBehavior>().angleOffset;
    }
}
