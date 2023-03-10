using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPopup : MonoBehaviour
{
    public GameObject header;
    public GameObject subtext;
    public TextMeshProUGUI headerTMP;
    public TextMeshProUGUI subtextTMP;

    public virtual void SetUp(PopupSpawner ps)
    {
        headerTMP = header.GetComponent<TextMeshProUGUI>();
        headerTMP.SetText(ps.title);
        subtextTMP = subtext.GetComponent<TextMeshProUGUI>();
        subtextTMP.SetText(ps.description);
    }
}
