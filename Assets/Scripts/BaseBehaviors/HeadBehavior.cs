using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehavior : RobotPart
{
    [Header("Head")]
    public int currentCharge = 3;
    public int maxCharge = 3;
    public bool canUse = true;

    [SerializeField] PlayerUI ui;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddCharge(1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddCharge(2);
        }
    }
    public virtual void UseHead()
    {
        if (canUse)
        {
            canUse = false;
            UseOverride();
            currentCharge = 0;
            ui.ResetCharge();
        }
    }

    public virtual void UseOverride()
    {

    }

    public virtual void AddCharge(int add)
    {
        currentCharge = Mathf.Clamp(currentCharge + add, 0, maxCharge);
        ui.IncrementCharge(add);
        if (currentCharge == maxCharge)
        {
            canUse = true;
        }
    }

    public override bool OnPartPickUp(GameObject player)
    {
        bool retVal = base.OnPartPickUp(player);
        ui = player.GetComponent<PlayerBody>().cm.playerUI;
        ui.UpdateEquip(this.gameObject);
        return retVal;
    }

    public override bool OnPartDrop(GameObject player)
    {
        bool retVal = base.OnPartDrop(player);
        player.GetComponent<PlayerBody>().cm.playerUI.UpdateEquip(null);
        return retVal;
    }
}
