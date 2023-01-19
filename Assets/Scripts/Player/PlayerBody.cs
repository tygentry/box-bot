using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBody : MonoBehaviour
{
    [Header("Robot Components")]
    public GameObject headObj;
    public List<GameObject> bodyObjs = new List<GameObject>();
    public GameObject legsObj;
    private HeadBehavior head;
    private List<BodyBehavior> bodies;
    private LegBehavior legs;

    private CanvasManager cm;

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

        cm = FindObjectOfType<CanvasManager>();
    }

    public HeadBehavior GetHead() { return head; }
    public BodyBehavior GetBody(int index) { if (index >= 0 && index < bodies.Count) return bodies[index]; else return null; }
    public LegBehavior GetLeg() { return legs; }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            cm.ToggleCustomizeMenu();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            cm.MatchPlayer(this);
        }

        foreach (BodyBehavior b in bodies)
        {
            // holding / initial press
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                b.PressLeft(Time.deltaTime);
            }
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                b.PressRight(Time.deltaTime);
            }

            // release
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                b.ReleaseLeft(Time.deltaTime);
            }
            if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                b.ReleaseRight(Time.deltaTime);
            }
        }
    }
}
