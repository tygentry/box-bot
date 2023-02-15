using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [System.Serializable]
    public enum ModifiableStats
    {
        None,
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

    public void ModifyStat(ModifiableStats stat, float value)
    {
        if (stat == ModifiableStats.None) return;
        statsDict[stat] += value;
    }

    public float GetStat(ModifiableStats stat)
    {
        if (stat == ModifiableStats.None) return 0.0f;
        return statsDict[stat];
    }
}
