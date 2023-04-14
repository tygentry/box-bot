using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartAttribute : MonoBehaviour
{
    [SerializeField] Image attrIcon;
    [SerializeField] TextMeshProUGUI attrText;

    public void SetAttribute(Attributes.RobotPartAttributes attr)
    { 
        if (Attributes.AttributeImages.Count == 0)
            Attributes.PopulateImages();

        attrIcon.sprite = Attributes.AttributeImages[attr];
        attrText.text = Attributes.AttributeText[attr];
    }
}
