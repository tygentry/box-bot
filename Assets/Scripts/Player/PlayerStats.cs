using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum ModifiableStats
    {
        None,
        AllDamage,
        MeleeDamage,
        ShotDamage,
        ChargeDamage,
        AttackSpeed,
        HeadChargeRate,
        Luck
    }

    [SerializeField] Dictionary<ModifiableStats, float> stats = new Dictionary<ModifiableStats, float>()
    {
        { ModifiableStats.AllDamage, 1.0f },
        { ModifiableStats.MeleeDamage, 1.0f },
        { ModifiableStats.ShotDamage, 1.0f },
        { ModifiableStats.ChargeDamage, 1.0f },
        { ModifiableStats.AttackSpeed, 1.0f },
        { ModifiableStats.HeadChargeRate, 1.0f },
        { ModifiableStats.Luck, 1.0f },
    };

    public void ModifyStat(ModifiableStats stat, float value)
    {
        if (stat == ModifiableStats.None) return;
        stats[stat] += value;
    }

    public float GetStat(ModifiableStats stat)
    {
        if (stat == ModifiableStats.None) return 0.0f;
        return stats[stat];
    }
}
