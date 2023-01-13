using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleShotArm : ArmBehavior
{
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void HoldAttack(float dt)
    {
        
    }

    public override void ReleaseAttack(float dt)
    {
        Fire();
    }

    public void Fire()
    {
        Instantiate(projectile);
    }
}
