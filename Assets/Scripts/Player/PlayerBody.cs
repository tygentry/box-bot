using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class PlayerBody : MonoBehaviour
{
    [Header("Debug / Start Parts")]
    public bool useStartParts = true;
    [SerializeField] GameObject headPrefab;
    [SerializeField] List<StartBody> bodyPrefabs;
    [SerializeField] GameObject legsPrefab;

    [System.Serializable]
    public struct StartBody
    {
        public GameObject leftArm;
        public GameObject trinket;
        public GameObject rightArm;
    }

    [Header("Robot Components")]
    public GameObject headObj;
    public List<GameObject> bodyObjs = new List<GameObject>();
    public GameObject legsObj;
    private HeadBehavior head;
    private List<BodyBehavior> bodies;
    private LegBehavior legs;

    public CanvasManager cm;
    [SerializeField] PlayerMovement mv;
    [SerializeField] MouseTracker mt;
    private Controls controls;
    private bool pauseStatus;

    public void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null) { return; }
        controls.Enable();
    }

    private void OnDisable()
    {
        if (controls == null) { return; }
        controls.Disable();
    }

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
        if (useStartParts)
        {
            SpawnStartParts();
        }
        cm.MatchPlayer(this);
    }

    //needs to be expanded for multiple bodies
    public void SpawnStartParts()
    {
        UpdateBody(HeadLocString(), headPrefab);
        for (int i = 0; i < bodyPrefabs.Count; i++)
        {
            UpdateBody(LeftArmLocString() + "_" + i, bodyPrefabs[i].leftArm);
            UpdateBody(RightArmLocString() + "_" + i, bodyPrefabs[i].rightArm);
            UpdateBody(TrinketLocString() + "_" + i, bodyPrefabs[i].trinket);
        }
        UpdateBody(LegsLocString(), legsPrefab);
    }

    public HeadBehavior GetHead() { return head; }
    public BodyBehavior GetBody(int index) { if (index >= 0 && index < bodies.Count) return bodies[index]; else return null; }
    public LegBehavior GetLeg() { return legs; }

    // Update is called once per frame
    void Update()
    {
        if (controls.PlayerControls.OpenCustomize.triggered)
        {
            cm.ToggleCustomizeMenu();
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

                //head use
                if (mv.controls.PlayerControls.UseHeadActive.triggered)
                {
                    head.UseHead();
                }
            }
        }
    }

    /*
     * Matches a DropZone to the PlayerBody variable so all updates can be made to track a body change
     */
    public string MatchPart(DropZone slot)
    {
        if (slot == null) return "";

        GameObject part = slot.associatedSlot;
        if (part == headObj) return HeadLocString();
        if (part == legsObj) return LegsLocString();
        for (int i = 0; i < bodies.Count; i++)
        {
            if (part == bodies[i].leftArmObj) return LeftArmLocString() + "_" + i;
            if (part == bodies[i].coreTrinketObj) return TrinketLocString() + "_" + i;
            if (part == bodies[i].rightArmObj) return RightArmLocString() + "_" + i;
        }

        return "";
    }

    /*
     * "Picks up" a given part and slots it into the location, attaching it to the player, and updating UI pointers
     */
    public GameObject UpdateBody(string location, GameObject newPartPrefab)
    {
        if (location.Equals("")) return null;

        //base setup for any new part
        GameObject newPart = Instantiate(newPartPrefab);
        RobotPart prefabBase = newPart.GetComponent<RobotPart>();
        Interactable newPartBase = newPart.GetComponent<Interactable>();
        newPartBase.DisableInteraction();
        prefabBase.OnPartPickUp(gameObject);

        int sepLoc = location.IndexOf("_");
        // head or legs
        if (sepLoc == -1)
        {
            if (location.Equals(HeadLocString()))
            {
                if (headObj)
                {
                    prefabBase.CopyPart(headObj.GetComponent<RobotPart>());
                    Destroy(headObj);
                }
                headObj = newPart;
                head = headObj.GetComponent<HeadBehavior>();
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                return headObj;
            }
            else if (location.Equals(LegsLocString()))
            {
                if (legsObj)
                {
                    prefabBase.CopyPart(legsObj.GetComponent<RobotPart>());
                    Destroy(legsObj);
                }
                legsObj = newPart;
                legs = legsObj.GetComponent<LegBehavior>();
                newPart.transform.SetParent(this.gameObject.transform, false);
                newPart.transform.SetLocalPositionAndRotation(prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                return legsObj;
            }
        }
        //arm or trinket
        else
        {
            string slotName = location.Substring(0, sepLoc);
            int index = Int16.Parse(location.Substring(sepLoc + 1));
            BodyBehavior b = GetBody(index);
            if (b == null)
            {
                Debug.Log("We have a serious problem");
            }
            if (slotName.Equals(LeftArmLocString()))
            {
                if (b.leftArmObj)
                {
                    prefabBase.CopyPart(b.leftArmObj.GetComponent<RobotPart>());
                    Destroy(b.leftArmObj);
                }
                b.leftArmObj = newPart;
                b.UpdateLeftArm();
                newPart.transform.SetParent(b.gameObject.transform, false);
                Vector3 adjustedPos = b.armPos + prefabBase.startPos;
                adjustedPos.x *= -1; //flipping X value for left arm
                Vector3 adjustedRot = prefabBase.startRot;
                adjustedRot.y += 180;
                newPart.transform.SetLocalPositionAndRotation(adjustedPos, Quaternion.Euler(adjustedRot));
                return b.leftArmObj; 
            }
            else
            {
                if (slotName.Equals(RightArmLocString()))
                {
                    if (b.rightArmObj)
                    {
                        prefabBase.CopyPart(b.rightArmObj.GetComponent<RobotPart>());
                        Destroy(b.rightArmObj);
                    }
                    b.rightArmObj = newPart;
                    b.UpdateRightArm();
                    newPart.transform.SetParent(b.gameObject.transform, false);
                    newPart.transform.SetLocalPositionAndRotation(b.armPos + prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                    return b.rightArmObj;
                }
                else if (slotName.Equals(TrinketLocString()))
                {
                    if (b.coreTrinketObj)
                    {
                        prefabBase.CopyPart(b.coreTrinketObj.GetComponent<RobotPart>());
                        Destroy(b.coreTrinketObj);
                    }
                    b.coreTrinketObj = newPart;
                    b.UpdateTrinketArm();
                    newPart.transform.SetParent(b.gameObject.transform, false);
                    newPart.transform.SetLocalPositionAndRotation(b.trinketPos + prefabBase.startPos, Quaternion.Euler(prefabBase.startRot));
                    return b.coreTrinketObj;
                }
            }
        }

        return null;
    }

    /*
     * Removes the equipped item from the location slot, dropping the part on the ground, and updating the UI pointers
     */
    public void Unequip(string location)
    {
        if (location.Equals("")) return;

        GameObject droppedPrefab = null;

        int sepLoc = location.IndexOf("_");
        // head or legs
        if (sepLoc == -1)
        {
            if (location.Equals(HeadLocString()))
            {
                droppedPrefab = headObj;
                headObj = null;
                head = null;
            }
            else if (location.Equals(LegsLocString()))
            {
                droppedPrefab = legsObj;
                legsObj = null;
                legs = null;
            }
        }
        //arm or trinket
        else
        {
            string slotName = location.Substring(0, sepLoc);
            int index = Int16.Parse(location.Substring(sepLoc + 1));
            BodyBehavior b = GetBody(index);
            if (slotName.Equals(LeftArmLocString()))
            {
                droppedPrefab = b.leftArmObj;
                b.leftArmObj = null;
                b.UpdateLeftArm();
            }
            else
            {
                if (slotName.Equals(RightArmLocString()))
                {
                    droppedPrefab = b.rightArmObj;
                    b.rightArmObj = null;
                    b.UpdateRightArm();
                }
                else if (slotName.Equals(TrinketLocString()))
                {
                    droppedPrefab = b.coreTrinketObj;
                    b.coreTrinketObj = null;
                    b.UpdateTrinketArm();
                }
            }
        }

        if (droppedPrefab != null)
        {
            droppedPrefab.GetComponent<RobotPart>().OnPartDrop(gameObject);
            GameObject drop = Instantiate(droppedPrefab);
            drop.GetComponent<Interactable>().EnableInteraction();
            drop.transform.position = gameObject.transform.position;
            drop.transform.rotation = Quaternion.Euler(0, 0, 0);
            Destroy(droppedPrefab);
        }
    }

    public void SetArmMovement(bool status)
    {
        //flips the change val to update locks - status=true will remove one lock count, false will add one lock
        int changeVal = status ? -1 : 1;
        foreach (BodyBehavior b in bodies)
        {
            if (b.GetLeftArm())
                b.GetLeftArm().followMouse += changeVal;
            if (b.GetRightArm())
                b.GetRightArm().followMouse += changeVal;
        }
    }

    public void DefaultArms()
    {
        foreach (BodyBehavior b in bodies)
        {
            if (b.GetLeftArm())
                b.GetLeftArm().ZeroArm();
            if (b.GetRightArm())
                b.GetRightArm().ZeroArm();
        }
    }

    public void SetMouseFollow(bool status)
    {
        mt.followPlayer = status;
    }

    /**
     * Handles updating anything necessary to stop calculation during a pause to simulate a pause (i.e. stopping arm rotation)
     */
    public void SimulatePause(bool newStatus)
    {
        pauseStatus = newStatus;
        //tell all arms to stop following mouse
        SetArmMovement(!pauseStatus);
    }

    public static string HeadLocString() { return "headObj"; }
    public static string LeftArmLocString() { return "leftArmObj"; }
    public static string RightArmLocString() { return "rightArmObj"; }
    public static string TrinketLocString() { return "coreTrinketObj"; }
    public static string LegsLocString() { return "legsObj"; }
}
