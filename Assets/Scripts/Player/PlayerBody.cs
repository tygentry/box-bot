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

    public CanvasManager cm;
    [SerializeField] PlayerMovement mv;

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
        mv = gameObject.GetComponent<PlayerMovement>();
    }

    public HeadBehavior GetHead() { return head; }
    public BodyBehavior GetBody(int index) { if (index >= 0 && index < bodies.Count) return bodies[index]; else return null; }
    public LegBehavior GetLeg() { return legs; }

    // Update is called once per frame
    void Update()
    {
        if (mv.controls.PlayerControls.OpenCustomize.triggered)
        {
            cm.ToggleCustomizeMenu();
        }

        //DEBUG
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            cm.MatchPlayer(this);
        }

        if (!cm.isCustomizing)
        {
            foreach (BodyBehavior b in bodies)
            {
                // holding / initial press
                if (mv.controls.PlayerControls.LeftFirePress.triggered)
                {
                    b.PressLeft(Time.deltaTime);
                }
                if (mv.controls.PlayerControls.RightFirePress.triggered)
                {
                    b.PressRight(Time.deltaTime);
                }

                // release
                if (mv.controls.PlayerControls.LeftFireRelease.triggered)
                {
                    b.ReleaseLeft(Time.deltaTime);
                }
                if (mv.controls.PlayerControls.RightFireRelease.triggered)
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

    public void UpdateBody(GameObject newPartPrefab, DropZone slot)
    {
        string location = MatchPart(slot);
        if (location.Equals("")) return;

        //base setup for any new part
        RobotPart prefabBase = newPartPrefab.GetComponent<RobotPart>();
        GameObject newPart = Instantiate(newPartPrefab);
        Interactable newPartBase = newPart.GetComponent<Interactable>();
        newPartBase.DisableInteraction();

        int sepLoc = location.IndexOf("_");
        // head or legs
        if (sepLoc == -1)
        {
            if (location.Equals("headObj"))
            {
                Destroy(headObj);
                headObj = newPart;
                head = headObj.GetComponent<HeadBehavior>();
                slot.associatedSlot = headObj;
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
            }
            else if (location.Equals("legsObj"))
            {
                Destroy(legsObj);
                legsObj = newPart;
                legs = legsObj.GetComponent<LegBehavior>();
                slot.associatedSlot = legsObj;
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
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
                Destroy(b.leftArmObj);
                b.leftArmObj = newPart;
                b.UpdateLeftArm();
                slot.associatedSlot = b.leftArmObj;
                newPart.transform.SetParent(b.gameObject.transform, false);
                Vector3 adjustedPos = prefabBase.startPos;
                adjustedPos.x *= -1; //flipping X value for left arm
                Vector3 adjustedRot = prefabBase.startRot;
                adjustedRot.z += 180;
                newPart.transform.SetLocalPositionAndRotation(adjustedPos, Quaternion.Euler(adjustedRot));
            }
            else
            {
                if (slotName.Equals("rightArmObj"))
                {
                    Destroy(b.rightArmObj);
                    b.rightArmObj = newPart;
                    b.UpdateRightArm();
                    slot.associatedSlot = b.rightArmObj;
                    newPart.transform.SetParent(b.gameObject.transform, false);
                    newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                }
                else if (slotName.Equals("coreTrinketObj"))
                {
                    Destroy(b.coreTrinketObj);
                    b.coreTrinketObj = newPart;
                    b.UpdateTrinketArm();
                    slot.associatedSlot = b.coreTrinketObj;
                    newPart.transform.SetParent(b.gameObject.transform, false);
                    newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                }
            }
        }
    }

    public void Unequip(GameObject droppedPrefab, DropZone slot)
    {
        string location = MatchPart(slot);
        if (location.Equals("")) return;

        GameObject drop = Instantiate(droppedPrefab);
        drop.transform.position = gameObject.transform.position;

        int sepLoc = location.IndexOf("_");
        // head or legs
        if (sepLoc == -1)
        {
            if (location.Equals("headObj"))
            {
                Destroy(headObj);
                headObj = null;
                head = null;
                slot.associatedSlot = headObj;
            }
            else if (location.Equals("legsObj"))
            {
                Destroy(legsObj);
                legsObj = null;
                legs = null;
                slot.associatedSlot = legsObj;
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
                Destroy(b.leftArmObj);
                b.leftArmObj = null;
                b.UpdateLeftArm();
                slot.associatedSlot = b.leftArmObj;
            }
            else
            {
                if (slotName.Equals("rightArmObj"))
                {
                    Destroy(b.rightArmObj);
                    b.rightArmObj = null;
                    b.UpdateRightArm();
                    slot.associatedSlot = b.rightArmObj;
                }
                else if (slotName.Equals("coreTrinketObj"))
                {
                    Destroy(b.coreTrinketObj);
                    b.coreTrinketObj = null;
                    b.UpdateTrinketArm();
                    slot.associatedSlot = b.coreTrinketObj;
                }
            }
        }
    }
}
