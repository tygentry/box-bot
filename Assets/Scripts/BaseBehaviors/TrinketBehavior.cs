using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinketBehavior : RobotPart
{
    [Header("Trinket")]
    //Enterable list for applying various stat modifiers for trinkets (MUST BE IN DECIMAL FORM (.15) NOT PERCENTAGE (15%)
    [SerializeField] public PlayerStats.StatDict[] statsList;

    [SerializeField] Dictionary<PlayerStats.ModifiableStats, float> statChangers = new Dictionary<PlayerStats.ModifiableStats, float>();

    private new void Start()
    {
        base.Start();
        statChangers.Clear();
        for (int i = 0; i < statsList.Length; i++)
        {
            statChangers.Add(statsList[i].stat, statsList[i].value);
        }
    }

    public override bool OnPartPickUp(GameObject player)
    {
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
