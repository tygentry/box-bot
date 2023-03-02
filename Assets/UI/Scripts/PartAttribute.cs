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
        attrIcon.sprite = Resources.Load(Attributes.AttributeImages[attr]) as Sprite;
        attrText.text = Attributes.AttributeText[attr];
    }
}
