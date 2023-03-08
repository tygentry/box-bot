using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartPopup : InteractPopup
{
    [Header("Robot Part")]
    [SerializeField] GameObject attributePanel;
    [SerializeField] GameObject linkedPart;
    public RobotPart.PartEnum type;
    [SerializeField] List<Attributes.RobotPartAttributes> attributes;
    [SerializeField] GameObject attributePrefab;

    // Start is called before the first frame update
    void Start()
    {
        headerTMP.text = headerTMP.text + " - " + type;
        foreach (var attr in attributes)
        {
            GameObject newAttr = Instantiate(attributePrefab, attributePanel.transform);
            newAttr.GetComponent<PartAttribute>().SetAttribute(attr);
        }
    }
}
