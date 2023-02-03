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

    public float popoutDistance = 220.0f;
    RectTransform trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<RectTransform>();
    }

    public void MimicCustomize(CustomizeMenu cm)
    {
        if (cm.headSlot.transform.GetChild(0))
        {
            head.SetImage(cm.headSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
        }
        for (int i = 0; i < bodies.Count; i++)
        {
            if (cm.leftArmSlots[i].transform.GetChild(0))
            {
                bodies[i].leftArm.SetImage(cm.leftArmSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
            if (cm.trinketSlots[i].transform.GetChild(0))
            {
                bodies[i].trinket.SetImage(cm.trinketSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
            if (cm.rightArmSlots[i].transform.GetChild(0))
            {
                bodies[i].rightArm.SetImage(cm.rightArmSlots[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
            }
        }
        if (cm.legsSlot.transform.GetChild(0))
        {
            legs.SetImage(cm.legsSlot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite);
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
