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
    void Start()
    {
        trans = gameObject.GetComponent<RectTransform>();
        cm = FindObjectOfType<CanvasManager>();
    }

    public void NewPart(PopoutButton clicked)
    {
        GameObject newPart = cm.customizeMenu.player.GetComponent<PlayerMovement>().interactedObj;
        if (newPart == null) { return; }
        string loc = FindSlot(clicked);
        cm.customizeMenu.player.Unequip(loc);
        cm.customizeMenu.player.UpdateBody(loc, newPart);
        //still not connecting to visuals
        cm.customizeMenu.CalibrateDropZones();
        MimicCustomize();
        Destroy(newPart);
        cm.customizeMenu.player.GetComponent<PlayerMovement>().interactedObj = null;
    }

    private string FindSlot(PopoutButton button)
    {
        if (button == null) return "";

        if (button == head) return "headObj";
        if (button == legs) return "legsObj";
        for (int i = 0; i < bodies.Count; i++)
        {
            if (button == bodies[i].leftArm) return "leftArmObj_" + i;
            if (button == bodies[i].trinket) return "coreTrinketObj_" + i;
            if (button == bodies[i].rightArm) return "rightArmObj_" + i;
        }

        return "";
    }

    public void MimicCustomize()
    {
        if (cm.customizeMenu.headSlot.transform.childCount == 0) { head.SetImage(transparentSprite); }
        else
        {
            head.SetImage(cm.customizeMenu.headSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            if (cm.customizeMenu.leftArmSlots[i].transform.childCount == 0) { bodies[i].leftArm.SetImage(transparentSprite); }
            else
            {
                bodies[i].leftArm.SetImage(cm.customizeMenu.leftArmSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
            if (cm.customizeMenu.trinketSlots[i].transform.childCount == 0) { bodies[i].trinket.SetImage(transparentSprite); }
            else
            {
                bodies[i].trinket.SetImage(cm.customizeMenu.trinketSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
            if (cm.customizeMenu.rightArmSlots[i].transform.childCount == 0) { bodies[i].rightArm.SetImage(transparentSprite); }
            else
            {
                bodies[i].rightArm.SetImage(cm.customizeMenu.rightArmSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
        }
        if (cm.customizeMenu.legsSlot.transform.childCount == 0) { legs.SetImage(transparentSprite); }
        else
        {
            legs.SetImage(cm.customizeMenu.legsSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
        }
    }

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
