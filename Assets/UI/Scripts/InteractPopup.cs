using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPopup : MonoBehaviour
{
    public string headerText;
    public string subtextText;
    public GameObject header;
    public GameObject subtext;
    public TextMeshProUGUI headerTMP;
    public TextMeshProUGUI subtextTMP;

    // Start is called before the first frame update
    public void Start()
    {
        print(gameObject.name);
        headerTMP = header.GetComponent<TextMeshProUGUI>();
        headerTMP.SetText(headerText);
        subtextTMP = subtext.GetComponent<TextMeshProUGUI>();
        subtextTMP.SetText(subtextText);
    }
}
