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

    private bool spacePressed = false;

    [Header("Interact")]
    public GameObject closestIntObj;
    public bool canInteract = true;
    public GameObject interactedObj;

    private Vector2 dodgeDirection = Vector2.zero;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        legBehavior = GetComponentInChildren<LegBehavior>();
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        spacePressed = false;
        input = Vector2.zero;
        input.x = controls.PlayerControls.MovementHorizontal.ReadValue<float>();
        input.y = controls.PlayerControls.MovementVertical.ReadValue<float>();

        if (controls.PlayerControls.SpaceBar.triggered)
        {
            spacePressed = true;
        }

        legBehavior.LegUpdate(input, spacePressed);

        if (controls.PlayerControls.Interact.triggered && canInteract && closestIntObj != null)
        {
            interactedObj = closestIntObj;
            closestIntObj.GetComponent<Interactable>().Interact();
        }
    }

    private void FixedUpdate()
    {
        legBehavior.LegFixedUpdate();
    }
}
