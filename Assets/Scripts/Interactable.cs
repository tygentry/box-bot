using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canHighlight = true;
    bool isHighlighted = false;
    public Material highlightMat;
    public Shader nonHighlightShader;
    public Shader highlightShader;

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
            highlightMat.shader = highlightShader; //SWAP OVER TO COROUTINE

            yield return new WaitForSeconds(timeUntilPopup);

            if (isHighlighted)
            { 
                InvokeRepeating("DisplayPopup", 1.0f, 0.01f);
            }
        }
        else
        {
            highlightMat.shader = nonHighlightShader;
            InvokeRepeating("HidePopup", 1.0f, 0.01f);
        }
    }

    public void DisplayPopup()
    {
        popupGroup.alpha += (1.0f / (1.0f / 0.01f));
    }

    public void HidePopup()
    {
        popupGroup.alpha -= (1.0f / (2.0f / 0.1f));
    }
}
