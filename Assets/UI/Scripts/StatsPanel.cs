using System.Collections.Generic;
using System;
using UnityEngine;
using static PlayerStats;

public class StatsPanel : MonoBehaviour
{
    public GameObject StatLinePrefab;

    private PlayerStats playerStats;
    public static Dictionary<ModifiableStats, GameObject> StatLines = new Dictionary<ModifiableStats, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerStats>();
        playerStats.statsPanel = this;
        foreach (ModifiableStats stat in Enum.GetValues(typeof(ModifiableStats)))
        {
            GameObject sl = Instantiate(StatLinePrefab, gameObject.transform);
            sl.GetComponent<StatLine>().SetUp(stat, playerStats.GetStat(stat));
            StatLines.Add(stat, sl);
        }
    }

    public void UpdateAllStats()
    {
        foreach (ModifiableStats stat in Enum.GetValues(typeof(ModifiableStats)))
        {
            UpdateStat(stat);
        }
    }

    public void UpdateStat(ModifiableStats stat)
    {
        StatLines[stat].GetComponent<StatLine>().SetVal(playerStats.GetStat(stat));
    }
}
