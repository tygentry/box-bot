using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyBehavior : RobotPart
{
    [Header("Body Components")]
    public GameObject leftArmObj;
    public GameObject rightArmObj;
    public GameObject coreTrinketObj;
    private ArmBehavior leftArm;
    private ArmBehavior rightArm;
    private TrinketBehavior trinket;

    [Header("Part Offsets")]
    public Vector3 headPos;
    public Vector3 trinketPos;
    public Vector3 armPos; // no need for left arm pos, just flips x to mirror
    public Vector3 legsPos;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        if (leftArmObj)
        {
            leftArm = leftArmObj.GetComponent<ArmBehavior>();
        }
        if (rightArmObj)
        {
            rightArm = rightArmObj.GetComponent<ArmBehavior>();
        }
        if (coreTrinketObj)
        {
            trinket = coreTrinketObj.GetComponent<TrinketBehavior>();
        }
    }

    public void UpdateArms(Vector3 dir)
    {
        if (leftArm)
            leftArm.MoveArm(dir);
        if (rightArm)
            rightArm.MoveArm(dir);
    }

    #region getters/setters
    public ArmBehavior GetLeftArm() { return leftArm; }
    public void UpdateLeftArm() 
    { 
        if (leftArmObj == null)
        {
            leftArm = null;
        }
        else
        {
            leftArm = leftArmObj.GetComponent<ArmBehavior>();
        }
    }
    public ArmBehavior GetRightArm() { return rightArm; }
    public void UpdateRightArm()
    {
        if (rightArmObj == null)
        {
            rightArm = null;
        }
        else
        {
            rightArm = rightArmObj.GetComponent<ArmBehavior>();
        }
    }
    public TrinketBehavior GetTrinket() { return trinket; }
    public void UpdateTrinketArm()
    {
        if (coreTrinketObj == null)
        {
            trinket = null;
        }
        else
        {
            trinket = coreTrinketObj.GetComponent<TrinketBehavior>();
        }
    }
    #endregion

    //click handlers
    public void PressLeft(float dt) { if (leftArm) leftArm.PressAttack(dt); }
    public void PressRight(float dt) { if (rightArm) rightArm.PressAttack(dt); }
    public void ReleaseLeft(float dt) { if (leftArm) leftArm.ReleaseAttack(dt); }
    public void ReleaseRight(float dt) { if (rightArm) rightArm.ReleaseAttack(dt); }
}
