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
        foreach (ModifiableStats stat in Enum.GetValues(typeof(ModifiableStats)))
        {
            GameObject sl = Instantiate(StatLinePrefab, gameObject.transform);
            StatLines.Add(stat, sl);
        }
    }

    public void UpdateStats()
    {

    }
}
