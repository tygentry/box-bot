using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBehavior : RobotPart
{
    [Header("Trinket")]
    //Enterable list for applying various stat modifiers for trinkets (MUST BE IN DECIMAL FORM (.15) NOT PERCENTAGE (15%)
    [SerializeField] public PlayerStats.StatDict[] statsList;

    [SerializeField] Dictionary<PlayerStats.ModifiableStats, float> statChangers = null;

    private void SetupDict()
    {
        statChangers = new Dictionary<PlayerStats.ModifiableStats, float>();
        for (int i = 0; i < statsList.Length; i++)
        {
            statChangers.Add(statsList[i].stat, statsList[i].value);
        }
    }

    public override bool OnPartPickUp(GameObject player)
    {
        bool retVal = base.OnPartPickUp(player);
        SetupDict();
        foreach (KeyValuePair<PlayerStats.ModifiableStats, float> statMod in statChangers)
        {
            stats.ModifyStat(statMod.Key, statMod.Value);
        }
        return retVal;
    }

    public override bool OnPartDrop(GameObject player)
    {
        bool retVal = base.OnPartDrop(player);
        SetupDict();
        foreach (KeyValuePair<PlayerStats.ModifiableStats, float> statMod in statChangers)
        {
            stats.ModifyStat(statMod.Key, -statMod.Value);
        }
        return retVal;
    }
}
