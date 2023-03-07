using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float timeHovered = 0.0f;
    public float hoverToShowTime = 1.0f;
    public string title;
    [TextArea(15, 20)]
    public string description;
    public bool isHovered = false;
    bool isShown = false;

    [SerializeField]
    public GameObject popupPrefab;
    [SerializeField]
    public GameObject spawnLocation;

    private GameObject popup;

    public bool isHoverable = true;

    // Update is called once per frame
    void Update()
    {
        if (isHovered)
        {
            timeHovered += Time.deltaTime;
            //display popup
            if (timeHovered > 1.0f && !isShown)
            {
                isShown = true;
                SpawnPopUp();
            }
        }
        else
        {
            if (isShown)
            {
                DespawnPopUp();
                isShown = false;
            }
            timeHovered = 0.0f;
        }
    }

    public void SpawnPopUp()
    {
        if (popup != null)
        {
            Destroy(popup);
        }
        popup = Instantiate(popupPrefab, spawnLocation.transform);
        popup.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = title;
        popup.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = description;
        Animator anim = popup.GetComponent<Animator>();
        anim.SetBool("Show", true);
    }

    public void DespawnPopUp()
    {
        Animator anim = popup.GetComponent<Animator>();
        anim.SetBool("Show", false);
        print("destroy");
        StartCoroutine(DestroyOnVanish());
    }

    IEnumerator DestroyOnVanish()
    {
        yield return new WaitForSeconds(.4f);
        Destroy(popup);
        popup = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHoverable)
        {
            isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isHoverable)
        {
            isHovered = false;
        }
    }
}
