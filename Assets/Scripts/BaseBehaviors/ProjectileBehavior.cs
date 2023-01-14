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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //handle enemy damage / check here

        if (destroyOnHit)
        {
            Destroy(gameObject);
        }

        //handle bounce implementation here
    }
}
