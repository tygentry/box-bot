using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public Transform player;
    public float lookAheadDistance = 0.5f;
    public bool followPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            transform.position = Vector2.MoveTowards((Vector2)player.transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), lookAheadDistance);
        }
    }
}
