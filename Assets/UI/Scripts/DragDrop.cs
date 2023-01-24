using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    [Header("Canvas Objects")]
    public CanvasManager cm;

    [Header("Drop Zone Attributes")]
    public bool isDraggable = true;
    public GameObject dragger;
    public bool isOverDropZone = false;
    private GameObject previousParent;
    private int prevChildIndex;
    public List<GameObject> allowedDropZones = new List<GameObject>();
    public List<GameObject> dropZones = new List<GameObject>();
    private Vector2 startPosition;
    private RectTransform trans;
    public RobotPart.PartEnum dropType = RobotPart.PartEnum.None;
    [SerializeField] GameObject partPrefab;

    void Start()
    {
        trans = GetComponent<RectTransform>();
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
            centerOnDragger();
        }
    }

    private void centerOnDragger()
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
        float minDist = Vector3.Distance(gameObject.transform.position, retVal.transform.position);
        foreach (GameObject dropZone in valid)
        {
            float dist = Vector3.Distance(gameObject.transform.position, dropZone.transform.position);
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
                //if dropping on populated drop zone, swap the contents of the zones
                if (dropZone.transform.childCount > 0)
                {
                    Transform swapChild = dropZone.transform.GetChild(dropZone.transform.childCount - 1);
                    swapChild.SetParent(previousParent.transform, false);
                    swapChild.SetSiblingIndex(prevChildIndex);
                    //swapChild.localPosition = startPosition;
                    cm.customizeMenu.player.UpdateBody(swapChild.GetComponent<DragDrop>().partPrefab, previousParent.GetComponent<DropZone>());
                }
                //print(dropZone);
                trans.SetParent(dropZone.transform, false);
                cm.customizeMenu.player.UpdateBody(partPrefab, dropZone.GetComponent<DropZone>());
                if (dropZone == previousParent) 
                {
                    trans.SetSiblingIndex(prevChildIndex);
                }
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
}
