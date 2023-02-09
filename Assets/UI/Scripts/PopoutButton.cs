using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopoutButton : MonoBehaviour
{
    [SerializeField] CustomizePopout customPop;
    [SerializeField] Image partImg;
    public RobotPart.PartEnum type;

    public void SetImage(Sprite img) { partImg.sprite = img; }

    // generic call to let popout know a new part was picked up
    public void Selected()
    {
        customPop.NewPart(this);
    }
}
