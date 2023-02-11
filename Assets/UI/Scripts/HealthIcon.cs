using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcon : MonoBehaviour
{
    [SerializeField] Image fullHealthImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleImage()
    {
        fullHealthImage.enabled = !fullHealthImage.enabled;
    }
}
