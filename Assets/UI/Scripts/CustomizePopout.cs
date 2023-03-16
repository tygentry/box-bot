using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePopout : MonoBehaviour
{
    [Header("Popout Vars")]
    [SerializeField] PopoutButton head;
    [SerializeField] List<BodySelect> bodies = new List<BodySelect>();
    [SerializeField] PopoutButton legs;

    [SerializeField] Sprite transparentSprite;
    public float popoutDistance = 220.0f;
    RectTransform trans;
    private CanvasManager cm;

    // Start is called before the first frame update
    void Awake()
    {
        trans = gameObject.GetComponent<RectTransform>();
        cm = FindObjectOfType<CanvasManager>();
    }

    /*
     * Handler for when a button in the customize popout is clicked to pick up a new part
     */
    public void NewPart(PopoutButton clicked)
    {
        GameObject newPart = cm.customizeMenu.player.GetComponent<PlayerMovement>().interactedObj;

        if (newPart == null) { return; }
        if (newPart.GetComponent<RobotPart>().type != clicked.type) { return; }

        string loc = FindSlot(clicked);
        cm.customizeMenu.player.Unequip(loc);
        cm.customizeMenu.player.UpdateBody(loc, newPart);
        cm.customizeMenu.MatchCharacter(cm.customizeMenu.player);
        MimicCustomize();
        Destroy(newPart);
        cm.customizeMenu.player.GetComponent<PlayerMovement>().interactedObj = null;
    }

    /*
     * Determines the location of the button clicked based on its placement in the popout
     */
    private string FindSlot(PopoutButton button)
    {
        if (button == null) return "";

        if (button == head) return PlayerBody.HeadLocString();
        if (button == legs) return PlayerBody.LegsLocString();
        for (int i = 0; i < bodies.Count; i++)
        {
            if (button == bodies[i].leftArm) return PlayerBody.LeftArmLocString() + "_" + i;
            if (button == bodies[i].trinket) return PlayerBody.TrinketLocString() + "_" + i;
            if (button == bodies[i].rightArm) return PlayerBody.RightArmLocString() + "_" + i;
        }

        return "";
    }

    /*
     * Forces reset of customize popup to match customize menu
     */
    public void MimicCustomize()
    {
        if (cm.customizeMenu.headSlot.transform.childCount == 0) { head.SetImage(transparentSprite, false); }
        else
        {
            head.SetImage(cm.customizeMenu.headSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite, false);
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            if (cm.customizeMenu.leftArmSlots[i].transform.childCount == 0) { bodies[i].leftArm.SetImage(transparentSprite, false); }
            else
            {
                bodies[i].leftArm.SetImage(cm.customizeMenu.leftArmSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite, true);
            }
            if (cm.customizeMenu.trinketSlots[i].transform.childCount == 0) { bodies[i].trinket.SetImage(transparentSprite, false); }
            else
            {
                bodies[i].trinket.SetImage(cm.customizeMenu.trinketSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite, false);
            }
            if (cm.customizeMenu.rightArmSlots[i].transform.childCount == 0) { bodies[i].rightArm.SetImage(transparentSprite, false); }
            else
            {
                bodies[i].rightArm.SetImage(cm.customizeMenu.rightArmSlots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite, false);
            }
        }
        if (cm.customizeMenu.legsSlot.transform.childCount == 0) { legs.SetImage(transparentSprite, false); }
        else
        {
            legs.SetImage(cm.customizeMenu.legsSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite, false);
        }
    }

    //Animation control
    public void StartPopOut() { StopAllCoroutines(); StartCoroutine(PopOut()); }
    public void StartPopBack() { StopAllCoroutines(); StartCoroutine(PopBack()); }

    IEnumerator PopBack()
    {
        while (trans.anchoredPosition.x < popoutDistance)
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + 1f, trans.anchoredPosition.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator PopOut()
    {
        while (trans.anchoredPosition.x > -popoutDistance)
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x - 1f, trans.anchoredPosition.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
