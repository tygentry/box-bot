using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject customizeMenuObj;
    [SerializeField] GameObject customizePopoutObj;
    [SerializeField] GameObject playerUIObj;
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] GameObject playerDeathObj;
    public CustomizeMenu customizeMenu;
    public CustomizePopout customizePopout;
    public PlayerUI playerUI;
    public PauseMenuManager pauseMenuManager;
    public DeathScreen playerDeath;

    public bool isPaused;
    public bool isDead;
    public bool isCustomizing;

    private bool wasCustomizing;

    private PlayerMovement playerControls;
    private PlayerBody playerBody;

    private void Awake()
    {
        customizeMenu = customizeMenuObj.GetComponent<CustomizeMenu>();
        customizePopout = customizePopoutObj.GetComponent<CustomizePopout>();
        playerUI = playerUIObj.GetComponent<PlayerUI>();
        playerUI.cm = this;
        pauseMenuManager = pauseMenuObj.GetComponent<PauseMenuManager>();
        playerDeath = playerDeathObj.GetComponent<DeathScreen>();
        isPaused = false;
        isDead = false;
    }
    public void ToggleCustomizeMenu()
    {
        if (isDead)
            return;

        if (playerControls == null)
        {
            playerControls = customizeMenu.player.gameObject.GetComponent<PlayerMovement>();
            playerBody = customizeMenu.player.gameObject.GetComponent<PlayerBody>();
        }

        isCustomizing = !isCustomizing;
        if (isCustomizing) { playerControls.OnDisable(); }
        else { playerControls.OnEnable(); }
        customizeMenuObj.SetActive(isCustomizing);
    }

    public void MatchPlayer(PlayerBody p)
    {
        customizeMenu.MatchCharacter(p);
        customizePopout.MimicCustomize();
    }

    public void TogglePauseMenu()
    {
        if (isDead) return;

        if (playerControls == null)
        {
            playerControls = customizeMenu.player.gameObject.GetComponent<PlayerMovement>();
            playerBody = customizeMenu.player.gameObject.GetComponent<PlayerBody>();
        }
        
        if (isCustomizing)
        {
            wasCustomizing = true;
            ToggleCustomizeMenu();
            //isCustomizing = false;
        }
        else if (wasCustomizing)
        {
            wasCustomizing = false;
            ToggleCustomizeMenu();
            //isCustomizing = true;
        }

        isPaused = pauseMenuManager.TogglePause();
        playerBody.SimulatePause(isPaused);
        if (isPaused) { playerControls.OnDisable(); }
        else { playerControls.OnEnable(); }
    }

    public void PlayerDeath()
    {
        isDead = true;
        if (playerControls == null)
        {
            playerControls = customizeMenu.player.gameObject.GetComponent<PlayerMovement>();
            playerBody = customizeMenu.player.gameObject.GetComponent<PlayerBody>();
        }
        playerControls.OnDisable();
        playerDeath.ShowDeath();
    }
}
