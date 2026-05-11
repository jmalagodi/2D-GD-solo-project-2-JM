using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float shadowDamageRate = 10f;
    public float maxHealth = 100f;
    
    public GameOverManager gameOverManager;

    public Image healthBarFill;

    float health;
    bool inLight;

    void Start()
    {
        health = maxHealth;
        UpdateUI();
    }

    void Update()
    {
        if (!inLight)
        {
            health -= shadowDamageRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0f, maxHealth);
            UpdateUI();
        }

        if (health <= 0f)
        {
            gameOverManager.TriggerGameOver();
            enabled = false;
        }
    }

    void UpdateUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = health / maxHealth;
        }
    }

    public void InLight(bool state)
    {
        inLight = state;
    }
}