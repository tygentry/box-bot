using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizePopout : MonoBehaviour
{
    public float popoutDistance = 220.0f;
    RectTransform trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            StopAllCoroutines();
            StartCoroutine(PopOut());
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            StopAllCoroutines();
            StartCoroutine(PopBack());
        }
    }

    public void MimicCustomize(CustomizeMenu cm)
    {

    }

    IEnumerator PopBack()
    {
        while (trans.anchoredPosition.x < popoutDistance)
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x + 1f, trans.anchoredPosition.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator PopOut()
    {
        while (trans.anchoredPosition.x > -popoutDistance)
        {
            trans.anchoredPosition = new Vector2(trans.anchoredPosition.x - 1f, trans.anchoredPosition.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
