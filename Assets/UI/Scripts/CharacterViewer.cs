using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterViewer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool trackMouse = false;
    public PlayerBody player;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerBody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trackMouse)
        {
            //foreach (BodyBehavior b in player.bodies)
            {

            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        trackMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trackMouse= false;
    }
}
