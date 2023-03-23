using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyDropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject leftAngleSlider;
    [SerializeField] GameObject rightAngleSlider;

    public float leftAngle;
    public float rightAngle;

    public void ToggleLeftAngle()
    {
        leftAngleSlider.transform.SetAsLastSibling();
        leftAngle = leftAngleSlider.GetComponent<AngleSlider>().GetValue();
        leftAngleSlider.SetActive(!leftAngleSlider.activeSelf);
    }

    public void ToggleRightAngle()
    {
        rightAngleSlider.transform.SetAsLastSibling();
        rightAngle = rightAngleSlider.GetComponent<AngleSlider>().GetValue();
        rightAngleSlider.SetActive(!rightAngleSlider.activeSelf);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
