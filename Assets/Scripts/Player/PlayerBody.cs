using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [Header("Robot Components")]
    public GameObject headObj;
    public List<GameObject> bodyObjs = new List<GameObject>();
    public GameObject legsObj;
    private HeadBehavior head;
    private List<BodyBehavior> bodies;
    private LegBehavior legs;

    // Start is called before the first frame update
    void Start()
    {
        if (headObj)
        {
            head = headObj.GetComponent<HeadBehavior>();
        }
        bodies = new List<BodyBehavior>();
        foreach (GameObject b in bodyObjs)
        {
            bodies.Add(b.GetComponent<BodyBehavior>());
        }
        if (legsObj)
        {
            legs = legsObj.GetComponent<LegBehavior>();
        }
    }

    public HeadBehavior GetHead() { return head; }
    public BodyBehavior GetBody(int index) { return bodies[index]; }
    public LegBehavior GetLeg() { return legs; }

    // Update is called once per frame
    void Update()
    {
        foreach (BodyBehavior b in bodies)
        {
            // holding / initial press
            if (Input.GetButton("LeftAttack"))
            {
                b.HoldLeft(Time.deltaTime);
            }
            if (Input.GetButton("RightAttack"))
            {
                b.HoldRight(Time.deltaTime);
            }

            // release
            if (Input.GetButtonUp("LeftAttack"))
            {
                b.AttackLeft(Time.deltaTime);
            }
            if (Input.GetButtonUp("RightAttack"))
            {
                b.AttackRight(Time.deltaTime);
            }
        }
    }
}
