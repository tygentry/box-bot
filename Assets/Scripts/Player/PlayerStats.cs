using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Attributes;

public class PlayerStats : MonoBehaviour
{
    [System.Serializable]
    public enum ModifiableStats
    {
        AllDamage,
        MeleeDamage,
        ShotDamage,
        ChargeDamage,
        AttackSpeed,
        MoveSpeed,
        HeadChargeRate,
        Luck
    }

    [System.Serializable]
    public struct StatDict
    {
        public ModifiableStats stat;
        public float value;
    }

    public static Dictionary<ModifiableStats, Sprite> StatIcons = new Dictionary<ModifiableStats, Sprite>();
    //populating the icons for lookup
    private void Awake()
    {
        foreach (KeyValuePair<ModifiableStats, string> path in StatIconPaths)
        {
            StatIcons.Add(path.Key, Resources.Load(path.Value) as Sprite);
        }
    }

    public static Dictionary<ModifiableStats, string> StatIconPaths = new Dictionary<ModifiableStats, string>()
    {
        { ModifiableStats.AllDamage, "" },
        { ModifiableStats.MeleeDamage, "" },
        { ModifiableStats.ShotDamage, "" },
        { ModifiableStats.ChargeDamage, "" },
        { ModifiableStats.AttackSpeed, "" },
        { ModifiableStats.MoveSpeed, "" },
        { ModifiableStats.HeadChargeRate, "" },
        { ModifiableStats.Luck, "" },
    };

    public static Dictionary<ModifiableStats, string> StatNames = new Dictionary<ModifiableStats, string>()
    {
        { ModifiableStats.AllDamage, "Damage" },
        { ModifiableStats.MeleeDamage, "Melee Damage" },
        { ModifiableStats.ShotDamage, "Ranged Damage" },
        { ModifiableStats.ChargeDamage, "Charge Damage" },
        { ModifiableStats.AttackSpeed, "Attack Speed" },
        { ModifiableStats.MoveSpeed, "Move Speed" },
        { ModifiableStats.HeadChargeRate, "Special Charge" },
        { ModifiableStats.Luck, "Luck" },
    };

    [SerializeField] Dictionary<ModifiableStats, float> statsDict = new Dictionary<ModifiableStats, float>()
    {
        { ModifiableStats.AllDamage, 1.0f },
        { ModifiableStats.MeleeDamage, 1.0f },
        { ModifiableStats.ShotDamage, 1.0f },
        { ModifiableStats.ChargeDamage, 1.0f },
        { ModifiableStats.AttackSpeed, 1.0f },
        { ModifiableStats.MoveSpeed, 1.0f },
        { ModifiableStats.HeadChargeRate, 1.0f },
        { ModifiableStats.Luck, 1.0f },
    };

    public StatsPanel statsPanel;

    public void ModifyStat(ModifiableStats stat, float value)
    {
        statsDict[stat] += value;
        if (statsPanel) statsPanel.UpdateStat(stat);
    }

    public float GetStat(ModifiableStats stat)
    {
        return statsDict[stat];
    }

    public void PrintStats()
    {
        foreach (KeyValuePair<ModifiableStats, float> stat in statsDict)
        {
            Debug.Log(stat.Key + ": " + stat.Value);
        }
    }
}
