using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currenthealth;

    public void TakeDamage(float damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxHealth);
        print("Ouch, " + damage + " damage!");
    }

    public void Heal(float healAmount)
    {
        currenthealth = Mathf.Clamp(currenthealth + healAmount, 0, maxHealth);
    }
}
