using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupSpawner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Base Popup")]
    float timeHovered = 0.0f;
    public float hoverToShowTime = 1.0f;
    public string title;
    [TextArea(15, 20)]
    public string description;
    [SerializeField] Interactable interactable;

    [Header("Robot Part Popup")]
    public RobotPart.PartEnum type;
    public List<Attributes.RobotPartAttributes> partAttributes;

    public bool isHovered = false;
    bool isShown = false;

    [SerializeField]
    public GameObject popupPrefab;
    [SerializeField]
    public GameObject leftSpawnLocation;
    [SerializeField]
    public GameObject rightSpawnLocation;

    public GameObject popup;

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
        bool spawnLeft = SetLocation();
        print(spawnLeft);
        popup = Instantiate(popupPrefab, (spawnLeft ? leftSpawnLocation : rightSpawnLocation).transform);
        InteractPopup intP = popup.GetComponent<InteractPopup>();        
        intP.SetUp(this);
        Animator anim = popup.GetComponent<Animator>();
        anim.SetBool("Show", true);
    }

    public void DespawnPopUp()
    {
        if (popup == null) return;
        Animator anim = popup.GetComponent<Animator>();
        anim.SetBool("Show", false);
        StartCoroutine(DestroyOnVanish());
    }

    /**
     * Returns true if the popup should spawn to the left side, false if it should spawn to the right
     */
    private bool SetLocation()
    {
        float dir = gameObject.transform.position.x - interactable.GetPlayer().transform.position.x;
        print(dir);
        if (dir < 0.0f)
        {
            return true;
        }
        return false;
    }

    public void InstaDestroy()
    {
        Destroy(popup);
        popup = null;
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
