using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBehavior : RobotPart
{
    [SerializeField] public Dictionary<PlayerStats.ModifiableStats, float> statChangers = new Dictionary<PlayerStats.ModifiableStats, float>();

    public override bool OnPartPickUp()
    {
        foreach (KeyValuePair<PlayerStats.ModifiableStats, float> statMod in statChangers)
        {

        }
        return base.OnPartPickUp();
    }
}
