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
    [SerializeField] PopupSpawner popupSpawn;
    public float timeUntilPopup = 3.0f;
    [SerializeField] public Transform centerPt;

    public delegate bool InteractFunction();
    InteractFunction interact;
    public delegate bool PickUpFunction();
    PickUpFunction pickup;

    private bool isRobotPart;
    private GameObject player = null;

    public void Start()
    {
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
        popupSpawn.InstaDestroy();
    }

    public void EnableInteraction()
    {
        canHighlight = true;
        interactRange.enabled = true;
    }

    public void CoroutineToggle()
    {
        StartCoroutine(ToggleHighlight());
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

            if (isHighlighted && !popupSpawn.isShown)
            {
                popupSpawn.isShown = true;
                popupSpawn.SpawnPopUp();
            }
        }
        else
        {
            highlightMat.shader = nonHighlightShader;
            playerBody.cm.isCustomizing = false;
            playerBody.cm.customizePopout.StartPopBack();
            player.GetComponent<PlayerMovement>().interactedObj = null;
            popupSpawn.DespawnPopUp();
        }
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
                    mv.NewIntObj(this.gameObject);
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
                    mv.RemoveIntObj(this.gameObject);
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
