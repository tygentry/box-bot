using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerBody : MonoBehaviour
{
    [Header("Robot Components")]
    public GameObject headObj;
    public List<GameObject> bodyObjs = new List<GameObject>();
    public GameObject legsObj;
    private HeadBehavior head;
    private List<BodyBehavior> bodies;
    private LegBehavior legs;

    private CanvasManager cm;

    // Start is called before the first frame update
    void Start()
    {
        if (headObj)
        {
            head = headObj.GetComponent<HeadBehavior>();
        }
        bodies = new List<BodyBehavior>();
        foreach (GameObject b in bodyObjs)
        {
            bodies.Add(b.GetComponent<BodyBehavior>());
        }
        if (legsObj)
        {
            legs = legsObj.GetComponent<LegBehavior>();
        }

        cm = FindObjectOfType<CanvasManager>();
    }

    public HeadBehavior GetHead() { return head; }
    public BodyBehavior GetBody(int index) { if (index >= 0 && index < bodies.Count) return bodies[index]; else return null; }
    public LegBehavior GetLeg() { return legs; }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            cm.ToggleCustomizeMenu();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            cm.MatchPlayer(this);
        }

        if (!cm.isCustomizing)
        {
            foreach (BodyBehavior b in bodies)
            {
                // holding / initial press
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    b.PressLeft(Time.deltaTime);
                }
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    b.PressRight(Time.deltaTime);
                }

                // release
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    b.ReleaseLeft(Time.deltaTime);
                }
                if (Mouse.current.rightButton.wasReleasedThisFrame)
                {
                    b.ReleaseRight(Time.deltaTime);
                }
            }
        }
    }

    /*
     * Matches a DropZone to the PlayerBody variable so all updates can be made to track a body change
     */
    private string MatchPart(DropZone slot)
    {
        if (slot == null) return "";

        GameObject part = slot.associatedSlot;
        if (part == headObj) return "headObj";
        if (part == legsObj) return "legsObj";
        for (int i = 0; i < bodies.Count; i++)
        {
            if (part == bodies[i].leftArmObj) return "leftArmObj_" + i;
            if (part == bodies[i].coreTrinketObj) return "coreTrinketObj_" + i;
            if (part == bodies[i].rightArmObj) return "rightArmObj_" + i;
        }

        return "";
    }

    public void UpdateBody(GameObject newPart, DropZone slot)
    {
        string location = MatchPart(slot);
        if (location.Equals("")) return;

        int sepLoc = location.IndexOf("_");
        // head or legs
        if (sepLoc == -1)
        {
            if (location.Equals("headObj"))
            {
                headObj = newPart;
                head = headObj.GetComponent<HeadBehavior>();
                slot.associatedSlot = headObj;
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetPositionAndRotation(newPart.GetComponent<HeadBehavior>().startPos, Quaternion.Euler(newPart.GetComponent<HeadBehavior>().startRot));
            }
            else if (location.Equals("legsObj"))
            {
                legsObj = newPart;
                legs = legsObj.GetComponent<LegBehavior>();
                slot.associatedSlot = legsObj;
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetPositionAndRotation(newPart.GetComponent<LegBehavior>().startPos, Quaternion.Euler(newPart.GetComponent<LegBehavior>().startRot));
            }
        }
        //arm or trinket
        else
        {
            string slotName = location.Substring(0, sepLoc);
            int index = Int16.Parse(location.Substring(sepLoc + 1));
            BodyBehavior b = GetBody(index);
            if (slotName.Equals("leftArmObj"))
            {
                b.leftArmObj = newPart;
                b.UpdateLeftArm();
                slot.associatedSlot = b.leftArmObj;
                newPart.transform.SetParent(b.gameObject.transform, false);
                Vector3 adjustedPos = newPart.GetComponent<ArmBehavior>().startPos;
                adjustedPos.x *= -1; //flipping X value for left arm
                newPart.transform.SetPositionAndRotation(adjustedPos, Quaternion.Euler(newPart.GetComponent<ArmBehavior>().startRot));
            }
            else
            {
                if (slotName.Equals("rightArmObj"))
                {
                    b.rightArmObj = newPart;
                    b.UpdateRightArm();
                    slot.associatedSlot = b.rightArmObj;
                    newPart.transform.SetParent(this.gameObject.transform, false);
                    newPart.transform.SetPositionAndRotation(newPart.GetComponent<ArmBehavior>().startPos, Quaternion.Euler(newPart.GetComponent<ArmBehavior>().startRot));
                }
                else if (slotName.Equals("coreTrinketObj"))
                {
                    b.coreTrinketObj = newPart;
                    b.UpdateTrinketArm();
                    slot.associatedSlot = b.coreTrinketObj;
                    newPart.transform.SetParent(this.gameObject.transform, false);
                    newPart.transform.SetPositionAndRotation(newPart.GetComponent<TrinketBehavior>().startPos, Quaternion.Euler(newPart.GetComponent<TrinketBehavior>().startRot));
                }
            }
        }
    }
}
