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

    // Start is called before the first frame update
    public void Start()
    {
        gameObject.GetComponent<Interactable>().SetInteractable(OnPartPickup);
    }

    public virtual bool OnPartPickup() { Debug.Log("Base Robot Part Pickup"); return true; }
}
