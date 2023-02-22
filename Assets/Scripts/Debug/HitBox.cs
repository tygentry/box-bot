using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitBox : MonoBehaviour
{
    public bool DebugMode = false;
    public Collider2D hitbox; //when setting up a hitbox, must choose between box or circle collider attached and put into script
    public GameObject displayBox;
    [System.Serializable]
    public class HitBoxEvent : UnityEvent<Collider2D> { }
    public HitBoxEvent onHit;
    public UnityEvent afterEnd;

    // Start is called before the first frame update
    void Start()
    {
        hitbox.enabled = false;
    }

    private void Awake()
    {
        if (onHit == null)
        {
            onHit = new HitBoxEvent();
            Debug.Log("HitBox onHit event not set in " + gameObject.transform.parent.gameObject.name);
        }
        if (afterEnd == null)
        {
            afterEnd = new UnityEvent();
        }
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
        afterEnd.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for hittable tag here
        onHit.Invoke(collision);
    }
}
