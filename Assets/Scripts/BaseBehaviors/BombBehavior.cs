using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public float timer = 3.0f;
    public float activeHitboxTimer = 0.5f;
    public bool canHitPlayer = false;
    [SerializeField] HitBox explosionTrigger;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timer);
        explosionTrigger.Activate(activeHitboxTimer);
        yield return new WaitForSeconds(activeHitboxTimer);
        Destroy(gameObject);
    }
    

    public void Hit(Collider2D collision)
    {

    }
}
