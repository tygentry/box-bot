using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        MoveToPlayer();
    }

     public void MoveToPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
