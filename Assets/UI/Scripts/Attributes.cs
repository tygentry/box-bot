using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public enum RobotPartAttributes
    {
        Melee,
        Ranged,
        Charged,
    }

    public static Dictionary<RobotPartAttributes, string> AttributeImages = new Dictionary<RobotPartAttributes, string>()
    {
        { RobotPartAttributes.Melee, "" },
        { RobotPartAttributes.Ranged, "" },
        { RobotPartAttributes.Charged, "" },
    };

    public static Dictionary<RobotPartAttributes, string> AttributeText = new Dictionary<RobotPartAttributes, string>()
    {
        { RobotPartAttributes.Melee, "Melee" },
        { RobotPartAttributes.Ranged, "Ranged" },
        { RobotPartAttributes.Charged, "Charged" },
    };
}
