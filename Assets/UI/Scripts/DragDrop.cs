using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Canvas Objects")]
    public CanvasManager cm;

    [Header("Drop Zone Attributes")]
    public bool isDraggable = true;
    public GameObject dragger;
    public bool isOverDropZone = false;
    [SerializeField] GameObject objectParent;
    private GameObject previousParent;
    private int prevChildIndex;
    public List<GameObject> allowedDropZones = new List<GameObject>();
    public List<GameObject> dropZones = new List<GameObject>();
    private Vector2 startPosition;
    private RectTransform trans;
    public RobotPart.PartEnum dropType = RobotPart.PartEnum.None;
    [SerializeField] GameObject partPrefab;
    [SerializeField] PopupSpawner ps;

    void Start()
    {
        trans = objectParent.GetComponent<RectTransform>();
        Dragger d = FindObjectOfType<Dragger>();
        if (d != null)
        {
            dragger = d.gameObject;
        }
        cm = FindObjectOfType<CanvasManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZones.Add(collision.gameObject);
        //print(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        dropZones.Remove(collision.gameObject);
        if (dropZones.Count == 0)
        {
            isOverDropZone = false;
        }
    }

    public void StartDrag()
    {
        /*ModifierPopUp pop = GetComponent<ModifierPopUp>();
        if (pop != null)
        {
            pop.popup.isHoverable = false;
            pop.popup.isHovered = false;
        }*/
        dropZones.Clear();
        if (isDraggable)
        {
            startPosition = trans.localPosition;
            previousParent = trans.parent.gameObject;
            prevChildIndex = trans.GetSiblingIndex();
            dragger.GetComponent<Dragger>().isDragging = true;
            trans.localPosition = new Vector3(0, 0, 0);
            trans.SetParent(dragger.transform, false);
            CenterOnDragger();
        }
    }

    private void CenterOnDragger()
    {
        trans.anchorMax = new Vector2(0.5f, 0.5f);
        trans.anchorMin = new Vector2(0.5f, 0.5f);
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
    }

    private GameObject GetClosestValidDropZone()
    {
        List<GameObject> valid = new List<GameObject>();
        GameObject retVal = null;
        //first performing validation checks

        foreach (GameObject dropZone in dropZones)
        {
            DropZone dz = dropZone.GetComponent<DropZone>();
            if (dropZone != previousParent && (dz != null && dz.CheckAllowDrop(gameObject))) //prevents dropping onto same parent and check is the DropZone script is present, asking it if drop is valid)
            {
                if (allowedDropZones.Count == 0 || allowedDropZones.Contains(dropZone)) //if no specific drop zones are specified, goes to any, otherwise only to specified
                {
                    valid.Add(dropZone);
                }
            }
        }

        if (valid.Count == 0)
        {
            return retVal;
        }

        //then picking minimum distance from valid collision
        retVal = valid[0];
        float minDist = Vector3.Distance(objectParent.transform.position, retVal.transform.position);
        foreach (GameObject dropZone in valid)
        {
            float dist = Vector3.Distance(objectParent.transform.position, dropZone.transform.position);
            if (dist < minDist)
            {
                retVal = dropZone;
                minDist = dist;
            }
        }

        return retVal;
    }

    public void EndDrag()
    {
        if (isDraggable)
        {
            dragger.GetComponent<Dragger>().isDragging = false;
            GameObject dropZone = null;
            if (isOverDropZone)
            {
                dropZone = GetClosestValidDropZone();
            }
            //print(dropZone);

            if (isOverDropZone && dropZone != null) //prevents dropping onto same parent and check is the DropZone script is present, asking it if drop is valid
            {
                //unequip check
                if (dropZone.GetComponent<DropZone>().isUnequipZone)
                {
                    string unequipLoc = cm.customizeMenu.player.MatchPart(previousParent.GetComponent<DropZone>());
                    cm.customizeMenu.player.Unequip(unequipLoc);
                    previousParent.GetComponent<DropZone>().associatedSlot = null;
                    Destroy(gameObject);
                    cm.customizePopout.MimicCustomize();
                    return;
                }

                //if dropping on populated drop zone, swap the contents of the zones
                if (dropZone.transform.childCount > 0)
                {
                    Transform swapChild = dropZone.transform.GetChild(dropZone.transform.childCount - 1);
                    swapChild.SetParent(previousParent.transform, false);
                    swapChild.SetSiblingIndex(prevChildIndex);
                    //swapChild.localPosition = startPosition;
                    string swapLoc = cm.customizeMenu.player.MatchPart(previousParent.GetComponent<DropZone>());
                    //moving old part
                    previousParent.GetComponent<DropZone>().associatedSlot = cm.customizeMenu.player.UpdateBody(swapLoc, swapChild.GetComponentInChildren<DragDrop>().partPrefab);
                }
                //print(dropZone);
                trans.SetParent(dropZone.transform, false);
                string location = cm.customizeMenu.player.MatchPart(dropZone.GetComponent<DropZone>());
                //new part attach
                dropZone.GetComponent<DropZone>().associatedSlot = cm.customizeMenu.player.UpdateBody(location, partPrefab); ;
                if (dropZone == previousParent) 
                {
                    trans.SetSiblingIndex(prevChildIndex);
                }
                cm.MatchPlayer(cm.customizeMenu.player);
            }
            else
            {
                trans.SetParent(previousParent.transform, false);
                trans.SetSiblingIndex(prevChildIndex);
                trans.localPosition = startPosition;
            }

            /*ModifierPopUp pop = GetComponent<ModifierPopUp>();
            if (pop != null)
            {
                pop.popup.isHoverable = true;
            }*/
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ps.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ps.OnPointerExit(eventData);
    }
}
