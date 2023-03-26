using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : MonoBehaviour
{
    [Header("Component Slots")]
    public GameObject headSlot;
    public List<GameObject> bodySlots = new List<GameObject>();
    public GameObject legsSlot;
    private DropZone head;
    public List<DropZone> leftArms;
    public List<DropZone> trinkets;
    public List<DropZone> rightArms;
    private DropZone legs;

    public PlayerBody player;

    /*
     * Forces a recalculation of drop zone variables and associated slots on the player
     */
    public void CalibrateDropZones()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerBody>();
        }

        head = headSlot.GetComponent<DropZone>();
        head.associatedSlot = player.headObj;

        leftArms = new List<DropZone>();
        trinkets = new List<DropZone>();
        rightArms = new List<DropZone>();
        for (int i = 0; i < bodySlots.Count; i++)
        {
            BodyDropZone body = bodySlots[i].GetComponent<BodyDropZone>();
            body.cm = this;
            body.bodyNum = i;

            leftArms.Add(body.leftArm.GetComponent<DropZone>());
            leftArms[i].associatedSlot = player.GetBody(i).leftArmObj;

            trinkets.Add(body.trinket.GetComponent<DropZone>());
            trinkets[i].associatedSlot = player.GetBody(i).coreTrinketObj;

            rightArms.Add(body.rightArm.GetComponent<DropZone>());
            rightArms[i].associatedSlot = player.GetBody(i).rightArmObj;
        }

        legs = legsSlot.GetComponent<DropZone>();
        legs.associatedSlot = player.legsObj;
    }

    /*
     * Resets draggables placed in the customize menu so it can be repopulated
     */
    private void ClearDraggables()
    {
        DestroyAllChildren(headSlot);
        foreach (GameObject obj in bodySlots) 
        {
            BodyDropZone body = obj.GetComponent<BodyDropZone>();
            DestroyAllChildren(body.leftArm);
            DestroyAllChildren(body.trinket);
            DestroyAllChildren(body.rightArm);
        }
        DestroyAllChildren(legsSlot);
    }

    /*
     * Utility function to destroy all children of a gameobject
     */
    public static void DestroyAllChildren(GameObject obj)
    {
        if (obj == null) return;
    
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
        obj.transform.DetachChildren();
    }

    /*
     * Complete reset of Customize Menu based on the passed in PlayerBody
     */
    public void MatchCharacter(PlayerBody player)
    {
        ClearDraggables();
        CalibrateDropZones();
        if (player.GetHead())
        {
            Instantiate(player.GetHead().inventoryPrefab, headSlot.transform.position, Quaternion.identity).transform.SetParent(headSlot.transform);
        }
        for (int i = 0; i < bodySlots.Count; i++)
        {
            BodyBehavior body = player.GetBody(i);
            if (body == null) continue;
            if (body.GetLeftArm())
            {
                GameObject leftArm = Instantiate(body.GetLeftArm().inventoryPrefab, leftArms[i].transform.position, Quaternion.identity);
                leftArm.transform.SetParent(leftArms[i].transform);
                leftArm.transform.GetChild(0).gameObject.GetComponent<Image>().rectTransform.localScale = new Vector3(-1, 1, 1);
                body.GetLeftArm().angleOffset = bodySlots[i].GetComponent<BodyDropZone>().GetLeftAngle();
            }
            if (body.GetTrinket())
            {
                Instantiate(body.GetTrinket().inventoryPrefab, trinkets[i].transform.position, Quaternion.identity).transform.SetParent(trinkets[i].transform);
            }
            if (body.GetRightArm())
            {
                Instantiate(body.GetRightArm().inventoryPrefab, rightArms[i].transform.position, Quaternion.identity).transform.SetParent(rightArms[i].transform);
                body.GetRightArm().angleOffset = bodySlots[i].GetComponent<BodyDropZone>().GetRightAngle();
            }
        }
        if (player.GetLeg())
        {
            Instantiate(player.GetLeg().inventoryPrefab, legsSlot.transform.position, Quaternion.identity).transform.SetParent(legsSlot.transform);
        }
    }
}
