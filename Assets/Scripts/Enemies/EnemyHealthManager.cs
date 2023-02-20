using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currenthealth;

    private bool dead = false;

    public void Update()
    {
        if(currenthealth <= 0  && !dead)
        {
            KillEnemy();
        }
    }

    public void TakeDamage(float damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - damage, 0, maxHealth);
        print("Ouch, " + damage + " damage!");
    }

    public void Heal(float healAmount)
    {
        currenthealth = Mathf.Clamp(currenthealth + healAmount, 0, maxHealth);
    }

    public void KillEnemy()
    {
        dead = true;
        //Play enemy death animation
        Destroy(this.gameObject);
    }
}
