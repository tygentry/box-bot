using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool canHighlight = true;
    bool isHighlighted = false;
    public InteractPopup popup;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
    }
}
