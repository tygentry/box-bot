using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatLine : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI label;
    public TextMeshProUGUI value;

    public void SetUp(PlayerStats.ModifiableStats stat, float val)
    {
        icon.sprite = PlayerStats.StatIcons[stat];
        label.text = PlayerStats.StatNames[stat];
        SetVal(val);
    }

    public void SetVal(float val)
    {
        value.text = val.ToString("0.00");
    }
}
