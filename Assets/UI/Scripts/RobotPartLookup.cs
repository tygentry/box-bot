using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPartLookup : MonoBehaviour
{
    public static Dictionary<string, string> DisplayNames = new Dictionary<string, string>()
    {
        { "HammerMelee", "Hammer" },
        { "PelletGun", "Pellet Gun" },
        { "BombHead", "Mr. Bombastic" },
        { "BasicLegs", "Ol' Reliable" },
        { "RoFTrinket", "RoF Trinket"},
    };

    public static Dictionary<string, string> Descriptions = new Dictionary<string, string>()
    {
        { "HammerMelee", "Stop, hammer time!" },
        { "PelletGun", "If you can dodge a wrench..." },
        { "BombHead", "KABOOOM!" },
        { "BasicLegs", "That's it." },
        { "RoFTrinket", "Shoot fast, eat ass."},
    };

    //static definitions of common attribute combos for reuse
    static Attributes.RobotPartAttributes[] noAttributes = new Attributes.RobotPartAttributes[] { };

    public static Dictionary<string, Attributes.RobotPartAttributes[]> PartAttributes = new Dictionary<string, Attributes.RobotPartAttributes[]>()
    {
        { "HammerMelee", new Attributes.RobotPartAttributes[] { Attributes.RobotPartAttributes.Melee } },
        { "PelletGun", new Attributes.RobotPartAttributes[] { Attributes.RobotPartAttributes.Ranged } },
        { "BombHead", new Attributes.RobotPartAttributes[] { Attributes.RobotPartAttributes.Explosive } },
        { "BasicLegs", noAttributes },
        { "RoFTrinket", noAttributes },
    };
}
