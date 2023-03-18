using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    private GameObject attachedLegs;
    [SerializeField]
    private LegBehavior legBehavior;
    public Controls controls;
    public bool canMove;

    private bool spacePressed = false;
    private float staggerTimer = 0f;

    [Header("Interact")]
    public List<GameObject> intObjs = new List<GameObject>();
    public GameObject closestIntObj;
    public float closestIntObjDist = 999f;
    public bool canInteract = true;
    public GameObject interactedObj;

    private Vector2 dodgeDirection = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        controls = new Controls();
        closestIntObjDist = 999f;
    }

    public void OnEnable()
    {
        if (controls == null) { return; }
        controls.Enable();
    }

    public void OnDisable()
    {
        if (controls == null) { return; }
        controls.Disable();
    }

    public void SetLegMovement(LegBehavior legBehavior)
    {
        this.legBehavior = legBehavior;
    }

    // Update is called once per frame
    void Update()
    {
        staggerTimer -= Time.deltaTime;
        if (staggerTimer <= 0f && legBehavior != null)
            canMove = true;

        spacePressed = false;
        input = Vector2.zero;
        input.x = controls.PlayerControls.MovementHorizontal.ReadValue<float>();
        input.y = controls.PlayerControls.MovementVertical.ReadValue<float>();

        if (controls.PlayerControls.SpaceBar.triggered)
        {
            spacePressed = true;
        }

        if(canMove)
            legBehavior.LegUpdate(input, spacePressed);

        if (closestIntObj != null)
        {
            float checkDist = Vector3.Distance(closestIntObj.transform.position, gameObject.transform.position);
            if (checkDist > closestIntObjDist)
            {
                RecalculateClosestIntObj();
            }
            else
            {
                closestIntObjDist = checkDist;
            }
        }

        if (controls.PlayerControls.Interact.triggered && canInteract && intObjs.Count > 0)
        {
            interactedObj = closestIntObj;
            interactedObj.GetComponent<Interactable>().Interact();
        }
    }

    #region Interact
    // records a new object in range for interact and calculates to see if it is new closest
    public void NewIntObj(GameObject obj)
    {
        intObjs.Insert(0, obj);
        float dist = Vector3.Distance(obj.transform.position, gameObject.transform.position);
        if (dist < closestIntObjDist)
        {
            //toggle off old closest
            if (closestIntObj != null)
            {
                closestIntObj.GetComponent<Interactable>().CoroutineToggle();
            }
            //then setup new
            closestIntObjDist = dist;
            closestIntObj = obj;
            obj.GetComponent<Interactable>().CoroutineToggle();
        }
    }

    // removes an object from interact list once it is out of range and then recalculate if necessary
    public void RemoveIntObj(GameObject obj)
    {
        if (obj == null) { return; }
        if (closestIntObj == obj)
        {
            RecalculateClosestIntObj();
        }
        intObjs.Remove(obj);
    }

    // recalculates the closest obj between all in current range
    private void RecalculateClosestIntObj()
    {
        print("reacalc");
        //calculate new
        GameObject newClosestIntObj = null;
        closestIntObjDist = 999f;
        for (int i = 0; i < intObjs.Count; i++)
        {
            float dist = Vector3.Distance(intObjs[i].transform.position, gameObject.transform.position);
            if (dist < closestIntObjDist)
            {
                newClosestIntObj = intObjs[i];
                closestIntObjDist = dist;
            }
        }

        // if no change, don't worry about toggling off and on
        if (newClosestIntObj == closestIntObj) { return; }
        //removing old toggle
        if (closestIntObj != null)
        {
            closestIntObj.GetComponent<Interactable>().CoroutineToggle();
        }
        closestIntObj = newClosestIntObj;
        //enabling new toggle
        if (closestIntObj != null)
        {
            closestIntObj.GetComponent<Interactable>().CoroutineToggle();
        }
    }
    #endregion

    private void FixedUpdate()
    {
        if(canMove)
            legBehavior.LegFixedUpdate();
    }

    public void Stagger(float duration)
    {
        staggerTimer = duration;
        canMove = false;
    }
}
