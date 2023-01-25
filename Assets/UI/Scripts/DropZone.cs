using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public int allowedChildren;
    public RobotPart.PartEnum slotType = RobotPart.PartEnum.None;
    public GameObject associatedSlot;
    public bool isUnequipZone;

    public bool CheckAllowDrop(GameObject dropped)
    {
        if (dropped == null || !gameObject.activeSelf)
        {
            return false;
        }
        if (isUnequipZone) return true;

        bool isValid = true;
        /*if (transform.childCount >= allowedChildren && allowedChildren != -1)
        {
            isValid = false;
        }*/
        if (slotType != RobotPart.PartEnum.None)
        {
            if (slotType != dropped.GetComponent<DragDrop>().dropType)
            {
                isValid = false;
            }
        }

        return isValid;
    }
}
