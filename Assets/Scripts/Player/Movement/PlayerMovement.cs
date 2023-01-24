using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    private GameObject attachedLegs;
    private LegBehavior legBehavior;

    private bool dodge = false;
    private bool dodging = false;
    private bool invincible = false;

    [Header("Interact")]
    public GameObject intObj;
    public bool canInteract = true;

    private Vector2 dodgeDirection = Vector2.zero;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
           rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Vector2.zero;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && !dodging)
        {
            dodge = true;
            dodgeDirection = input;
        }

        if (Input.GetButtonDown("Interact") && canInteract && intObj != null)
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
       rb.MovePosition((Vector2)transform.position + input.normalized * legBehavior.regularSpeed * Time.deltaTime);
    }

    private void LegAction()
    {
        legBehavior.Dodge();
    }
}
