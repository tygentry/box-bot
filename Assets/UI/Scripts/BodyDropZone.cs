using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyDropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Drop Zones")]
    [SerializeField] public GameObject leftArm;
    [SerializeField] public GameObject trinket;
    [SerializeField] public GameObject rightArm;

    public CustomizeMenu cm;
    public int bodyNum;

    [Header("Angle Sliders")]
    [SerializeField] GameObject leftAngleSlider;
    [SerializeField] GameObject rightAngleSlider;

    public void ToggleLeftAngle()
    {
        leftAngleSlider.transform.SetAsLastSibling();
        leftAngleSlider.SetActive(!leftAngleSlider.activeSelf);
    }

    public void ToggleRightAngle()
    {
        rightAngleSlider.transform.SetAsLastSibling();
        rightAngleSlider.SetActive(!rightAngleSlider.activeSelf);
    }

    public void UpdateLeftAngle()
    {
        if (cm == null) { return; }
        cm.player.GetBody(bodyNum).GetLeftArm().angleOffset = GetLeftAngle();
    }

    public void UpdateRightAngle()
    {
        if (cm == null) { return; }
        cm.player.GetBody(bodyNum).GetRightArm().angleOffset = GetRightAngle();
    }

    #region getters and setters

    public float GetLeftAngle()
    {
        return leftAngleSlider.GetComponent<AngleSlider>().GetValue();
    }

    public float GetRightAngle()
    {
        return rightAngleSlider.GetComponent<AngleSlider>().GetValue();
    }

    public void SetLeftAngle(float value)
    {
        leftAngleSlider.GetComponent<AngleSlider>().SetValue(value);
    }

    public void SetRightAngle(float value)
    {
        rightAngleSlider.GetComponent<AngleSlider>().SetValue(value);
    }

    #endregion

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public float GetDropZoneAngle(DropZone dz)
    {
        if (dz == null) { return 0.0f; }
        if (dz.gameObject == leftArm)
        {
            return GetLeftAngle();
        }
        if (dz.gameObject == rightArm)
        {
            return GetRightAngle();
        }
        return 0.0f;
    }

    public void SetDropZoneAngle(DropZone dz, float val)
    {
        if (dz == null) { return; }
        if (dz.gameObject == leftArm)
        {
            SetLeftAngle(val);
        }
        if (dz.gameObject == rightArm)
        {
            SetRightAngle(val);
        }
    }
}
