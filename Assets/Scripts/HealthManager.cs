using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currenthealth;

    public void TakeDamage(int damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxHealth);
    }

    public void Heal(int healAmount)
    {
        currenthealth = Mathf.Clamp(currenthealth + healAmount, 0, maxHealth);
    }
}
