using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackController : MonoBehaviour
{
    public Rigidbody2D rb;
    
    public void KnockBack(Vector2 direction, float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
