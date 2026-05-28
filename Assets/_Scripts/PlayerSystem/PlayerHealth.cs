using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    PlayerConfig playerConfig;
    float currentHealth;
    bool isDead;
    
    public void Initialize(PlayerConfig config)
    {
        playerConfig = config;
        currentHealth = playerConfig.MaxHealth;
        isDead = false;

        GameEventSystem.InvokePlayerHealthChanged(currentHealth, playerConfig.MaxHealth);

    }

    public void TakeDamage(float damageAmount)
    {
        if(isDead)
            return;

        if(playerConfig == null)
        {
            Debug.LogError("Its necessary health configuration");
            return;
        }

        if(damageAmount <= 0f)
            return;
        
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, playerConfig.MaxHealth);
        GameEventSystem.InvokePlayerHealthChanged(currentHealth, playerConfig.MaxHealth);

        if(currentHealth <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        if (isDead)
            return;

        isDead = true;
        GameEventSystem.InvokeOnPlayerDied();
    }
    
}
