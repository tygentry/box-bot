using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float decayTime;
    public bool destroyOnHit;

    [Header("Bouncing Projectile")]
    public bool bounceOnWalls;
    public bool bounceOnEnemy;
    public int numBounces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
