using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject customizeMenuObj;
    [SerializeField] GameObject pauseMenu;
    public CustomizeMenu customizeMenu;

    public bool isPaused;
    public bool isCustomizing;

    private void Start()
    {
        customizeMenu = customizeMenuObj.GetComponent<CustomizeMenu>();
    }
    public void ToggleCustomizeMenu()
    {
        if (isPaused)
            return;

        isCustomizing = !isCustomizing;
        customizeMenuObj.SetActive(isCustomizing);
    }

    public void MatchPlayer(PlayerBody p)
    {
        customizeMenu.MatchCharacter(p);
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        if (isPaused)
        {

        }
        else
        {

        }
    }
}
