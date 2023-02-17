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
    public bool canMove = true;

    private bool spacePressed = false;
    private float staggerTimer = 0f;

    [Header("Interact")]
    public GameObject closestIntObj;
    public bool canInteract = true;
    public GameObject interactedObj;

    private Vector2 dodgeDirection = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        controls = new Controls();
    }

    private void OnEnable()
    {
        if (controls == null) { return; }
        controls.Enable();
    }

    private void OnDisable()
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
        if (staggerTimer <= 0f)
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

        if (controls.PlayerControls.Interact.triggered && canInteract && closestIntObj != null)
        {
            interactedObj = closestIntObj;
            closestIntObj.GetComponent<Interactable>().Interact();
        }
    }

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
