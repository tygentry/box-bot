using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] public CanvasManager cm;

    [Header("Health")]
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject healthIconPrefab;
    [SerializeField] List<HealthIcon> healthIcons;

    public int currentHealth = 3;
    public int maxHealth = 3;

    [Header("Charge")]
    [SerializeField] Slider chargeBar;
    [SerializeField] HeadBehavior headRef;

    public int currentCharge = 3;
    public int maxCharge = 3;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        
        PlayerHealthManager hm = player.GetComponent<PlayerHealthManager>();
        currentHealth = hm.currentHealth;
        maxHealth = hm.maxHealth;
        InitializeHealth();
    }

    public void ClearHealth()
    {
        CustomizeMenu.DestroyAllChildren(healthBar);
        healthIcons.Clear();
    }

    public void InitializeHealth()
    {
        ClearHealth();
        healthIcons = new List<HealthIcon>();
        for (int i = 0; i < maxHealth; i++)
        {
            healthIcons.Add(Instantiate(healthIconPrefab, healthBar.transform, false).GetComponent<HealthIcon>());
        }
    }

    public void HideUI() { gameObject.SetActive(false); }
    public void ShowUI() { gameObject.SetActive(true); }

    #region Health
    public void UpdateHealth(int numChanged)
    {
        int newHealth = Mathf.Clamp(currentHealth + numChanged, 0, maxHealth);
        if (currentHealth == newHealth) { return; }

        int dir = numChanged / Mathf.Abs(numChanged);
        for (int i = currentHealth; i != newHealth; i += dir)
        {
            if (dir > 0) { healthIcons[i].ToggleImage(); }
            else if (dir < 0) { healthIcons[i + dir].ToggleImage(); }
        }

        currentHealth = newHealth;
    }

    public void UpdateMaxHealth(int total)
    {
        //health cannot be 0 or less
        if (total <= 0) { return; }

        //will only update if change is noted
        int diff = total - maxHealth;
        if (diff == 0) { return; }
        if (diff > 0)
        {
            for (int i = maxHealth; i < total; i++)
            {
                GameObject newIcon = Instantiate(healthIconPrefab, healthBar.transform, false);
                newIcon.transform.SetAsFirstSibling();
                healthIcons.Insert(0, newIcon.GetComponent<HealthIcon>());
            }
            currentHealth += diff;
        }
        else
        {
            for (int i = maxHealth; i > total; i--)
            {
                Destroy(healthIcons[i - 1].gameObject);
                healthIcons.RemoveAt(i-1);
            }
            currentHealth += Mathf.Clamp(diff + (maxHealth - currentHealth),-99,0);
        }
        
        maxHealth = total;
    }
    #endregion

    #region Charge bar
    public void ResetCharge()
    {
        currentCharge = 0;
        chargeBar.value = 0;
    }

    public void IncrementCharge(int added)
    {
        currentCharge = Mathf.Clamp(currentCharge + added, 0, maxCharge);
        chargeBar.value = Mathf.Clamp(currentCharge / (float)maxCharge, 0.0f, 1.0f);
    }

    public void UpdateEquip(GameObject head)
    {
        if (head == null)
        {
            ResetCharge();
            return;
        }

        HeadBehavior newHead = head.GetComponent<HeadBehavior>();
        if (newHead == null) { return; } //didn't pass in a head

        headRef = newHead;
        currentCharge = headRef.currentCharge;
        maxCharge = headRef.maxCharge;
        IncrementCharge(0);
    }
    #endregion
}
