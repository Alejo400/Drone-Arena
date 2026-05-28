using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image healthFillImage;

    void OnEnable()
    {
        GameEventSystem.OnPlayerHealth += UpdateHealthBar;
    }

    void OnDisable()
    {
        GameEventSystem.OnPlayerHealth -= UpdateHealthBar;
    }

    void Awake()
    {
        if (healthFillImage == null)
            healthFillImage = GetComponent<Image>();

        if (healthFillImage != null)
            healthFillImage.fillAmount = 1f;
    }

    void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthFillImage == null)
            return;

        if (maxHealth <= 0f)
            return;

        healthFillImage.fillAmount = currentHealth / maxHealth;
    }
}