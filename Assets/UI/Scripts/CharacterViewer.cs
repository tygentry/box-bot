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
            //Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = (Vector2)Input.mousePosition;
            player.UpdateArms((Vector3)(mousePos - new Vector2(transform.position.x, transform.position.y)));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        trackMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trackMouse= false;
        player.DefaultArms();
    }
}
