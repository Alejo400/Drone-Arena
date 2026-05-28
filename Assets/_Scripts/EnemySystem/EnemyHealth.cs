using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    EnemyConfig enemyConfig;
    EnemyController enemyController;
    float currentHealth;
    bool isDead;

    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void Initialize(EnemyConfig config)
    {

        if(config == null)
        {
            Debug.LogError("EnemyConfig is required", this);
            enabled = false;
            return;
        }

        enemyConfig = config;
        enabled = true;
        currentHealth = enemyConfig.MaxHealth;
        isDead = false;
    }
    public void TakeDamage(float damageAmount)
    {
        if (isDead || enemyConfig == null)
            return;

        if (damageAmount <= 0f)
            return;

        currentHealth -= damageAmount;

        if (currentHealth < 0f)
            currentHealth = 0f;

        if (currentHealth <= 0f)
            Die();
    }
    void Die()
    {
        if (isDead)
            return;

        isDead = true;

        GameEventSystem.InvokeEnemyKilled(enemyConfig.PointsOnDeath);

        if (enemyController != null)
            enemyController.ReturnToPool();
        else
            Destroy(gameObject);
    }
}
