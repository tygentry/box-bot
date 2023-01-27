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

    [Header("Interact Popup")]
    public InteractPopup popupObj;
    public float timeUntilPopup = 3.0f;
    public CanvasGroup popupGroup;
    delegate bool InteractFunction();
    InteractFunction interact;

    [Header("Inventory")]
    public GameObject inventoryPrefab;
    public Vector3 startPos;
    public Vector3 startRot;

    public void Start()
    {
        //print("test");
        highlightMat = gameObject.GetComponent<SpriteRenderer>().material;
    }
    IEnumerator ToggleHighlight()
    {
        print(gameObject.name);
        isHighlighted = !isHighlighted;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canHighlight)
        {
            GameObject colObj = collision.gameObject;
            if (colObj.CompareTag("Player"))
            {
                colObj.GetComponent<PlayerMovement>().intObj = this.gameObject;
                StartCoroutine(ToggleHighlight());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canHighlight)
        {
            GameObject colObj = collision.gameObject;
            if (colObj.CompareTag("Player"))
            {
                if (colObj.GetComponent<PlayerMovement>().intObj == this.gameObject)
                {
                    colObj.GetComponent<PlayerMovement>().intObj = null;
                }
                StartCoroutine(ToggleHighlight());
            }
        }
    }

    public bool Interact()
    {
        print("interact");
        bool? retVal = interact?.Invoke();
        if (retVal != null)
        {
            return (bool)retVal;
        }
        return false;
    }
}
