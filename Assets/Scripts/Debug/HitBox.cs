using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public bool DebugMode = false;
    public Collider2D hitbox;
    public GameObject displayBox;
    public MeleeArm parentArm;

    // Start is called before the first frame update
    void Start()
    {
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(float time)
    {
        hitbox.enabled = true;
        if (DebugMode) displayBox.SetActive(true);
        StartCoroutine(AttackTime(time));
    }

    IEnumerator AttackTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (DebugMode) displayBox.SetActive(false);
        hitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for hittable tag here

        parentArm.Hit(collision);
    }
}
