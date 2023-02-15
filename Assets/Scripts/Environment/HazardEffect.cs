using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardEffect : MonoBehaviour
{
    public int hazardDamage = 1;
    public float knockbackForce = 50f;
    public float staggerDuration = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit Spike");

            collision.gameObject.GetComponent<PlayerMovement>().Stagger(staggerDuration);

            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
            print(direction);

            collision.gameObject.GetComponent<KnockBackController>().KnockBack(direction, knockbackForce);
            print("KnockBack");
            
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(hazardDamage);
        }
    }
}
