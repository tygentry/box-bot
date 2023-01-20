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

    // Start is called before the first frame update
    void Start()
    {
        CalibrateDropZones();
    }

    private void CalibrateDropZones()
    {
        head = headSlot.GetComponent<DropZone>();
        leftArms = new List<DropZone>();
        foreach (GameObject obj in leftArmSlots) { leftArms.Add(obj.GetComponent<DropZone>()); }
        trinkets = new List<DropZone>();
        foreach (GameObject obj in trinketSlots) { trinkets.Add(obj.GetComponent<DropZone>()); }
        rightArms = new List<DropZone>();
        foreach (GameObject obj in rightArmSlots) { rightArms.Add(obj.GetComponent<DropZone>()); }
        legs = legsSlot.GetComponent<DropZone>();
    }

    private void ClearDraggables()
    {
        DestroyAllChildren(headSlot);
        foreach (GameObject obj in leftArmSlots) { DestroyAllChildren(obj); }
        foreach (GameObject obj in trinketSlots) { DestroyAllChildren(obj); }
        foreach (GameObject obj in rightArmSlots) { DestroyAllChildren(obj); }
        DestroyAllChildren(legsSlot);
    }

    private void DestroyAllChildren(GameObject obj)
    {
        if (obj == null) return;
        /*while (obj.transform.childCount > 0)
        {
            GameObject child = obj.transform.GetChild(0).gameObject;
            child.transform.parent = null;
            Destroy(child);
        }*/
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
        obj.transform.DetachChildren();
    }

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