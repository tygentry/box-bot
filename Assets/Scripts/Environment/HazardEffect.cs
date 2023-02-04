using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardEffect : MonoBehaviour
{
    public int hazardDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit Spike");
            // add code to push player back and damage them
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(hazardDamage);

        }
    }
}
