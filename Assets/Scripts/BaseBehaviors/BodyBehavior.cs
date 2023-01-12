using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehavior : Interactable
{
    [Header("Body Components")]
    public GameObject leftArmObj;
    public GameObject rightArmObj;
    public GameObject coreTrinketObj;
    private ArmBehavior leftArm;
    private ArmBehavior rightArm;
    private TrinketBehavior trinket;

    // Start is called before the first frame update
    void Start()
    {
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

    public ArmBehavior GetLeftArm() { return leftArm; }
    public ArmBehavior GetRightArm() { return rightArm; }
    public TrinketBehavior GetTrinket() { return trinket; }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AttackLeft() { if (leftArm) leftArm.Attack(); }
    //public void AttackRight() { if (rightArm) rightArm.Attack(); }
}
