using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject customizeMenuObj;
    [SerializeField] GameObject customizePopoutObj;
    [SerializeField] GameObject playerUIObj;
    [SerializeField] GameObject pauseMenuObj;
    public CustomizeMenu customizeMenu;
    public CustomizePopout customizePopout;
    public PlayerUI playerUI;
    public PauseMenuManager pauseMenuManager;

    public bool isPaused;
    public bool isCustomizing;

    private void Awake()
    {
        customizeMenu = customizeMenuObj.GetComponent<CustomizeMenu>();
        customizePopout = customizePopoutObj.GetComponent<CustomizePopout>();
        playerUI = playerUIObj.GetComponent<PlayerUI>();
        pauseMenuManager = pauseMenuObj.GetComponent<PauseMenuManager>();
        isPaused = false;
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
        customizePopout.MimicCustomize();
    }

    public void TogglePauseMenu()
    {
        isPaused = pauseMenuManager.TogglePause();
    }
}
