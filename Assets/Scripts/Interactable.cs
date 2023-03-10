using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Highlighting")]
    public bool canHighlight = true;
    bool isHighlighted = false;
    public Material highlightMat;
    public Shader nonHighlightShader;
    public Shader highlightShader;
    [SerializeField] CircleCollider2D interactRange;

    [Header("Interact Popup")]
    public InteractPopup popupObj;
    public float timeUntilPopup = 3.0f;
    public CanvasGroup popupGroup;
    public delegate bool InteractFunction();
    InteractFunction interact;
    public delegate bool PickUpFunction();
    PickUpFunction pickup;

    private bool isRobotPart;
    private GameObject player = null;

    public void Start()
    {
        //print("test");
        highlightMat = gameObject.GetComponent<SpriteRenderer>().material;
        highlightMat.shader = nonHighlightShader;
        isRobotPart = gameObject.GetComponent<RobotPart>();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    // toggle interactability
    public void DisableInteraction()
    {
        canHighlight = false;
        interactRange.enabled = false;
        InstaHidePopup();
    }

    public void EnableInteraction()
    {
        canHighlight = true;
        interactRange.enabled = true;
    }

    // Highlight and animation control for popup
    IEnumerator ToggleHighlight()
    {
        //print(gameObject.name);
        isHighlighted = !isHighlighted;
        PlayerBody playerBody = player.GetComponent<PlayerBody>();
        if (isHighlighted)
        {
            highlightMat.shader = highlightShader;

            yield return new WaitForSeconds(timeUntilPopup);

            if (isHighlighted)
            { 
                StartCoroutine(DisplayPopup());
            }
        }
        else
        {
            highlightMat.shader = nonHighlightShader;
            StopAllCoroutines();
            playerBody.cm.isCustomizing = false;
            playerBody.cm.customizePopout.StartPopBack();
            player.GetComponent<PlayerMovement>().interactedObj = null;
            StartCoroutine(HidePopup());
        }
    }

    IEnumerator DisplayPopup()
    {
        if (popupGroup == null) yield break;
        while (popupGroup.alpha < 1.0f)
        {
            popupGroup.alpha += .01f;
            Mathf.Clamp(popupGroup.alpha, -0.1f, 1.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator HidePopup()
    {
        if (popupGroup == null) yield break;
        while (popupGroup.alpha > 0.0f)
        {
            popupGroup.alpha -= .01f;
            Mathf.Clamp(popupGroup.alpha, -0.1f, 1.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void InstaHidePopup()
    {
        popupGroup.alpha = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHighlight)
        {
            GameObject colObj = collision.gameObject;
            if (colObj == player || colObj.CompareTag("Player"))
            {
                if (player == null) { player = colObj; }
                PlayerMovement mv = player.GetComponent<PlayerMovement>();
                if (mv.canInteract) { 
                    mv.closestIntObj = this.gameObject;
                    StartCoroutine(ToggleHighlight());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canHighlight)
        {
            GameObject colObj = collision.gameObject;
            if (colObj == player || colObj.CompareTag("Player"))
            {
                if (player == null) { player = colObj; }
                PlayerMovement mv = player.GetComponent<PlayerMovement>();
                if (mv.canInteract)
                {
                    if (mv.closestIntObj == this.gameObject)
                    {
                        mv.closestIntObj = null;
                    }
                    StartCoroutine(ToggleHighlight());
                }
            }
        }
    }

    // Custom interact function handling
    public void SetInteractable(InteractFunction intF)
    {
        interact = intF;
    }

    public bool Interact()
    {
        bool? retVal = interact?.Invoke();
        if (retVal != null)
        {
            return (bool)retVal;
        }
        return false;
    }
}
