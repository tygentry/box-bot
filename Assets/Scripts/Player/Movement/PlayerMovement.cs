using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float dodgeSpeed = 5f;
    public float dodgeTime = 0.75f;
    private Vector2 input;
    private GameObject attachedLegs;

    private bool dodging = false;
    private bool invincible = false;
    private float dodgeTimer = 0f;

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
        dodgeTimer -= Time.deltaTime;
        input = Vector2.zero;
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && !dodging)
        {
            dodgeTimer = dodgeTime;
            dodging = true;
            dodgeDirection = input;
            invincible= true;
        }

        if (Input.GetButtonDown("Interact") && canInteract && intObj != null)
        {
            intObj.GetComponent<Interactable>().Interact();
        }

        if (dodgeTimer <= 0f)
        {
            dodging = false;
            invincible = false;
        }
    }

    private void FixedUpdate()
    {

        if(!dodging)
        {
            //transform.position += (Vector3)input * movementSpeed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + input * movementSpeed * Time.deltaTime);
        }
        else
        {
            //transform.position += (Vector3)dodgeDirection.normalized * dodgeSpeed * Time.deltaTime;
            rb.MovePosition((Vector2)transform.position + dodgeDirection.normalized * dodgeSpeed * Time.deltaTime);
        }
    }

    private void LegAction()
    {
        //attachedLegs.LegAction();
    }
}
