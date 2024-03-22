using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image backHealthBar;
    public Image frontHealthBar;

    float maxHealth = 100f;
    float currentHealth = 100f;
    float fillSpeed = 5f; // Adjust the fill speed as needed
    float backFillDelay = 0.2f; // Delay for the back health bar to follow the front health bar

    float targetFillAmount; // Target fill amount for both health bars

    void Start()
    {
        UpdateHealthBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DecreaseHealth(Random.Range(5f, 20f));
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseHealth(Random.Range(5f, 20f));
        }

        UpdateHealthBar();
    }

    void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        targetFillAmount = healthPercentage;

        // Smoothly interpolate the front health bar's fill amount
        frontHealthBar.fillAmount = Mathf.Lerp(frontHealthBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

        // Smoothly interpolate the back health bar's fill amount with a delay
        backHealthBar.fillAmount = Mathf.Lerp(backHealthBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed * backFillDelay);
    }
}
