using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPopup : MonoBehaviour
{
    public string popupText;
    public GameObject text;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp = text.GetComponent<TextMeshProUGUI>();
        tmp.SetText(popupText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
