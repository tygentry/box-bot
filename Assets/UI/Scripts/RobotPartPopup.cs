using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartPopup : InteractPopup
{
    [Header("Robot Part")]
    [SerializeField] GameObject attributePanel;
    [SerializeField] GameObject attributePrefab;

    public override void SetUp(PopupSpawner ps)
    {
        base.SetUp(ps);
        headerTMP.text = headerTMP.text + " - " + ps.type;
        foreach (var attr in ps.partAttributes)
        {
            GameObject newAttr = Instantiate(attributePrefab, attributePanel.transform);
            newAttr.GetComponent<PartAttribute>().SetAttribute(attr);
        }
    }
}
