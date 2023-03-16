using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotPartPopup : InteractPopup
{
    [Header("Robot Part")]
    [SerializeField] GameObject attributePanel;
    [SerializeField] GameObject attributePrefab;

    public override void SetUp(PopupSpawner ps)
    {
        headerTMP = header.GetComponent<TextMeshProUGUI>();
        headerTMP.SetText(RobotPartLookup.DisplayNames[ps.lookupStr] + " - " + ps.type);
        subtextTMP = subtext.GetComponent<TextMeshProUGUI>();
        subtextTMP.SetText(RobotPartLookup.Descriptions[ps.lookupStr]);
        Attributes.RobotPartAttributes[] attrs = RobotPartLookup.PartAttributes[ps.lookupStr];
        for (int i = 0; i < attrs.Length; i++)
        {
            GameObject newAttr = Instantiate(attributePrefab, attributePanel.transform);
            newAttr.GetComponent<PartAttribute>().SetAttribute(attrs[i]);
        }
    }
}
