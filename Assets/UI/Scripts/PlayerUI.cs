using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject fullHealthIcon;
    [SerializeField] GameObject emptyHealthIcon;

    private int currentHealth = 3;
    private int maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HideUI() { gameObject.SetActive(false); }
    public void ShowUI() { gameObject.SetActive(true); }

    public void UpdateHealth(int numChanged)
    {
        for (int i = 0; i < numChanged && currentHealth < maxHealth; i++)
        {
            //spawn a new healthIcon to place in group
        }
    }

    public void UpdateMaxHealth(int total)
    {
        maxHealth = total;

        //spawn more instances of blank healthIcons
    }
}
