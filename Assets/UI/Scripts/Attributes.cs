using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class Attributes : MonoBehaviour
{
    public enum RobotPartAttributes
    {
        Melee,
        Ranged,
        Charged,
        Explosive,
    }

    public static Dictionary<RobotPartAttributes, Sprite> AttributeImages = new Dictionary<RobotPartAttributes, Sprite>();
    //populating the icons for lookup
    public static void PopulateImages()
    {
        foreach (KeyValuePair<RobotPartAttributes, string> path in AttributeImagesPaths)
        {
            AttributeImages.Add(path.Key, Resources.Load(path.Value) as Sprite);
        }
    }

    public static Dictionary<RobotPartAttributes, string> AttributeImagesPaths = new Dictionary<RobotPartAttributes, string>()
    {
        { RobotPartAttributes.Melee, "" },
        { RobotPartAttributes.Ranged, "" },
        { RobotPartAttributes.Charged, "" },
        { RobotPartAttributes.Explosive, "" },
    };

    public static Dictionary<RobotPartAttributes, string> AttributeText = new Dictionary<RobotPartAttributes, string>()
    {
        { RobotPartAttributes.Melee, "Melee" },
        { RobotPartAttributes.Ranged, "Ranged" },
        { RobotPartAttributes.Charged, "Charged" },
        { RobotPartAttributes.Explosive, "Explosive" },
    };
}
