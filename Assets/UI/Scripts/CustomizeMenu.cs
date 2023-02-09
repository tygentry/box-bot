using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenu : MonoBehaviour
{
    [Header("Component Slots")]
    public GameObject headSlot;
    public List<GameObject> leftArmSlots = new List<GameObject>();
    public List<GameObject> trinketSlots = new List<GameObject>();
    public List<GameObject> rightArmSlots = new List<GameObject>();
    public GameObject legsSlot;
    private DropZone head;
    private List<DropZone> leftArms;
    private List<DropZone> trinkets;
    private List<DropZone> rightArms;
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
        for (int i = 0; i < leftArmSlots.Count; i++)
        {
            leftArms.Add(leftArmSlots[i].GetComponent<DropZone>());
            leftArms[i].associatedSlot = player.GetBody(i).leftArmObj;
        }
        trinkets = new List<DropZone>();
        for (int i = 0; i < trinketSlots.Count; i++)
        {
            trinkets.Add(trinketSlots[i].GetComponent<DropZone>());
            trinkets[i].associatedSlot = player.GetBody(i).coreTrinketObj;
        }
        rightArms = new List<DropZone>();
        for (int i = 0; i < rightArmSlots.Count; i++)
        {
            rightArms.Add(rightArmSlots[i].GetComponent<DropZone>());
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
        foreach (GameObject obj in leftArmSlots) { DestroyAllChildren(obj); }
        foreach (GameObject obj in trinketSlots) { DestroyAllChildren(obj); }
        foreach (GameObject obj in rightArmSlots) { DestroyAllChildren(obj); }
        DestroyAllChildren(legsSlot);
    }

    /*
     * Utility function to destroy all children of a gameobject
     */
    public void DestroyAllChildren(GameObject obj)
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
        Instantiate(player.GetHead().inventoryPrefab, headSlot.transform.position, Quaternion.identity).transform.SetParent(headSlot.transform);
        for (int i = 0; i < trinkets.Count; i++)
        {
            BodyBehavior body = player.GetBody(i);
            GameObject leftArm = Instantiate(body.GetLeftArm().inventoryPrefab, leftArmSlots[i].transform.position, Quaternion.identity);
            leftArm.transform.SetParent(leftArmSlots[i].transform);
            leftArm.GetComponent<Image>().rectTransform.localScale = new Vector3(1,1,-1);
            Instantiate(body.GetTrinket().inventoryPrefab, trinketSlots[i].transform.position, Quaternion.identity).transform.SetParent(trinketSlots[i].transform);
            Instantiate(body.GetRightArm().inventoryPrefab, rightArmSlots[i].transform.position, Quaternion.identity).transform.SetParent(rightArmSlots[i].transform);
        }
        Instantiate(player.GetLeg().inventoryPrefab, legsSlot.transform.position, Quaternion.identity).transform.SetParent(legsSlot.transform);
    }
}
