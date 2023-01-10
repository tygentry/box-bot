using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ToggleHighlight());
        }
    }

    IEnumerator ToggleHighlight()
    {
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
        while (popupGroup.alpha < 1.0f)
        {
            popupGroup.alpha += .01f;
            Mathf.Clamp(popupGroup.alpha, -0.1f, 1.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator HidePopup()
    {
        while (popupGroup.alpha > 0.0f)
        {
            popupGroup.alpha -= .01f;
            Mathf.Clamp(popupGroup.alpha, -0.1f, 1.0f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ToggleHighlight());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(ToggleHighlight());
    }
}
