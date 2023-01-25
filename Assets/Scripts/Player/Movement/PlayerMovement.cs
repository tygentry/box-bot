using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    private GameObject attachedLegs;
    [SerializeField]
    private LegBehavior legBehavior;
    private Controls controls;

    private bool dodge = false;
    private bool dodging = false;
    private bool invincible = false;

    [Header("Interact")]
    public GameObject intObj;
    public bool canInteract = true;

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
        input = Vector2.zero;
        input.x = controls.PlayerControls.MovementHorizontal.ReadValue<float>();
        input.y = controls.PlayerControls.MovementVertical.ReadValue<float>();

        if (controls.PlayerControls.SpaceBar.triggered && !dodging)
        {
            dodge = true;
            dodgeDirection = input;
        }

        if (controls.PlayerControls.Interact.triggered && canInteract && intObj != null)
        {
            intObj.GetComponent<Interactable>().Interact();
        }
    }

    private void FixedUpdate()
    {
        if(dodge)
        {
            dodging = true;
            LegAction();
        }
       rb.MovePosition((Vector2)transform.position + legBehavior.regularSpeed * Time.deltaTime * input.normalized);
    }

    private void LegAction()
    {
        legBehavior.Dodge(input);
    }


}
