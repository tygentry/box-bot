using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [Header("Base Projectile")]
    private Rigidbody2D rb;
    public float speed;
    public float decayTime;
    public bool destroyOnHit;
    public int damage;
    

    [Header("Bouncing Projectile")]
    public bool bounceOnWalls;
    public bool bounceOnEnemy;
    public int numBounces;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        if (decayTime > 0)
        {
            Destroy(gameObject, decayTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("col");
        //handle enemy damage / check here
        if (collision.gameObject.transform.TryGetComponent<EnemyHealthManager>(out var hm))
        {
            hm.TakeDamage(damage);
        }

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }

        //handle bounce implementation here
    }
}
