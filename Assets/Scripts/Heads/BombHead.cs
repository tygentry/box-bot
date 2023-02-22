using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHead : HeadBehavior
{
    [SerializeField] GameObject bombPrefab;
    public override void UseOverride()
    {
        base.UseOverride();
        Instantiate(bombPrefab, transform.position, transform.rotation);
    }
}
