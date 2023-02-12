using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currenthealth;
    public PlayerUI playerUI;

    public void TakeDamage(int damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxHealth);
        print("Ouch, " + damage + " damage!");
        if (playerUI == null) { playerUI = gameObject.GetComponent<PlayerBody>().cm.playerUI; }
        playerUI.UpdateHealth(damage);
    }

    public void Heal(int healAmount)
    {
        currenthealth = Mathf.Clamp(currenthealth + healAmount, 0, maxHealth);
        if (playerUI == null) { playerUI = gameObject.GetComponent<PlayerBody>().cm.playerUI; }
        playerUI.UpdateHealth(healAmount);
    }
}
