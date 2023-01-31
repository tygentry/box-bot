using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopoutButton : MonoBehaviour
{
    [SerializeField] CustomizePopout customPop;
    [SerializeField] Image partImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetImage(Sprite img) { partImg.sprite = img; }

    public void Selected()
    {

    }
}
