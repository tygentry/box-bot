using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject customizeMenu;
    [SerializeField] GameObject pauseMenu;

    public bool isPaused;
    public bool isCustomizing;

    public void ToggleCustomizeMenu()
    {
        if (isPaused)
            return;

        isCustomizing = !isCustomizing;
        customizeMenu.SetActive(isCustomizing);
    }

    public void MatchPlayer()
    {

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
