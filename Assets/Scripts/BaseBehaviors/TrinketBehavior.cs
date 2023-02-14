using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBehavior : RobotPart
{
    [SerializeField] Dictionary<PlayerStats.ModifiableStats, float> statChangers = new Dictionary<PlayerStats.ModifiableStats, float>();

    public override bool OnPartPickUp(GameObject player)
    {
        print("trinketPickup");
        PlayerStats stats = player.GetComponent<PlayerStats>();
        foreach (KeyValuePair<PlayerStats.ModifiableStats, float> statMod in statChangers)
        {
            stats.ModifyStat(statMod.Key, statMod.Value);
        }
        return base.OnPartPickUp(player);
    }

    public override bool OnPartDrop(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        foreach (KeyValuePair<PlayerStats.ModifiableStats, float> statMod in statChangers)
        {
            stats.ModifyStat(statMod.Key, -statMod.Value);
        }
        return base.OnPartPickUp(player);
    }
}
