using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    EnemyConfig enemyConfig;
    IDamageable playerDamageable;
    bool isPlayerInside;

    public void Initialize(EnemyConfig config)
    {

        if(config == null)
            Debug.LogWarning("EnemyConfig is necessary to do damage", this);

        enemyConfig = config;
        playerDamageable = null;
        isPlayerInside = false;
    }

    void Update()
    {
        ApplyContactDamage();
    }
    //Apply Damage if player was detected on TriggerEnter / TriggerExit stop the damage 
    void ApplyContactDamage()
    {
        if (!isPlayerInside || playerDamageable == null || enemyConfig == null)
            return;

        float damage = enemyConfig.ContactDamagePerSecond * Time.deltaTime;
        playerDamageable.TakeDamage(damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerDamageable = playerHealth;
        isPlayerInside = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerDamageable = null;
        isPlayerInside = false;
    }
}
