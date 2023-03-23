using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    
    public float GetValue()
    {
        return slider.value;
    }
}
