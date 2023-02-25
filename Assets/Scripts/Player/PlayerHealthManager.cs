using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public PlayerUI playerUI;

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        print("Ouch, " + damage + " damage!");
        if (playerUI == null) { playerUI = gameObject.GetComponent<PlayerBody>().cm.playerUI; }
        playerUI.UpdateHealth(-damage);

        //death check
        if (currentHealth <= 0)
        {
            playerUI.cm.PlayerDeath();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(100);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        if (playerUI == null) { playerUI = gameObject.GetComponent<PlayerBody>().cm.playerUI; }
        playerUI.UpdateHealth(healAmount);
    }
}
