using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject healthIconPrefab;
    [SerializeField] List<HealthIcon> healthIcons;

    public int currentHealth = 3;
    public int maxHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        healthIcons = new List<HealthIcon>();
        for (int i = 0; i < maxHealth; i++)
        {
            healthIcons.Add(Instantiate(healthIconPrefab, healthBar.transform, false).GetComponent<HealthIcon>());
        }
    }

    private void Update()
    {   //debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpdateHealth(-2);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateHealth(2);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            UpdateMaxHealth(maxHealth-1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateMaxHealth(maxHealth+1);
        }
    }

    public void HideUI() { gameObject.SetActive(false); }
    public void ShowUI() { gameObject.SetActive(true); }

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
}
