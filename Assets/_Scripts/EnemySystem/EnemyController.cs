using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyContactDamage))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyConfig enemyConfig;

    EnemyMovement enemyMovement;
    EnemyHealth enemyHealth;
    EnemyContactDamage enemyContactDamage;
    EnemyPool enemyPool;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyContactDamage = GetComponent<EnemyContactDamage>();
    }

    public void Initialize(Transform playerTarget, EnemyConfig config)
    {

        if(playerTarget == null || config == null)
        {
            Debug.LogError("Target and Config are required", this);
            return;
        }

        enemyConfig = config;

        enemyMovement.Initialize(enemyConfig, playerTarget);
        enemyHealth.Initialize(enemyConfig);
        enemyContactDamage.Initialize(enemyConfig);
    }

    public void SetPool(EnemyPool pool)
    {
        enemyPool = pool;
    }

    public void ReturnToPool()
    {
        if (enemyPool == null)
        {
            Destroy(gameObject);
            return;
        }

        enemyPool.ReturnEnemy(this);
    }
}
