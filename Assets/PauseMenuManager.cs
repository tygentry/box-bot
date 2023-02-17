using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject menu;
    public CanvasManager canvasManager;
    private Controls controls;
    private bool paused = false;
    public bool allowPause = true;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if(controls.PlayerControls.Pause.triggered && allowPause)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if(allowPause)
        {
            if (!paused)
            {
                paused = true;
                menu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                paused = false;
                menu.SetActive(false);
                Time.timeScale = 1.0f;
            }
            canvasManager.ToggleCustomizeMenu();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
