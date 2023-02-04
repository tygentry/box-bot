using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currenthealth;
    public GameObject healthUI;

    public void TakeDamage(int damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxHealth);
        print("Ouch, " + damage + " damage!");
    }

    public void Heal(int healAmount)
    {
        currenthealth = Mathf.Clamp(currenthealth + healAmount, 0, maxHealth);
    }
}
