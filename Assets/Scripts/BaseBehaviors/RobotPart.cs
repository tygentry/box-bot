using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPart : MonoBehaviour
{
    public enum PartEnum { None, Head, Arm, Leg, Trinket };

    [Header("Inventory")]
    public PartEnum type;
    public GameObject inventoryPrefab;
    public Vector3 startPos;
    public Vector3 startRot;

    private Interactable interact;

    // Start is called before the first frame update
    public void Start()
    {
        interact = gameObject.GetComponent<Interactable>();
        interact.SetInteractable(OnPartPickup);
    }

    public virtual bool OnPartPickup() 
    {
        interact.GetPlayer()?.GetComponent<PlayerBody>().cm.customizePopout.StartPopOut();
        return true;
    }
}
