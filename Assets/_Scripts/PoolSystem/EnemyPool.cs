using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] int initialPoolSize = 20;
    [SerializeField] bool canExpand = true;

    readonly List<EnemyController> enemies = new();
    bool isInitialized;

    void Awake()
    {
        if (enemyPrefab != null)
            Initialize(enemyPrefab, initialPoolSize);
    }

    public bool Initialize(EnemyController prefab, int poolSize)
    {
        if (isInitialized)
            return true;

        if (prefab == null)
        {
            Debug.LogError("EnemyPrefab is required", this);
            return false;
        }

        enemyPrefab = prefab;
        initialPoolSize = Mathf.Max(0, poolSize);

        CreateInitialPool();
        isInitialized = true;

        return true;
    }

    void CreateInitialPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateEnemy();
        }
    }

    EnemyController CreateEnemy()
    {
        EnemyController enemy = Instantiate(enemyPrefab, transform);
        enemy.SetPool(this);
        enemy.gameObject.SetActive(false);

        enemies.Add(enemy);

        return enemy;
    }

    public EnemyController GetEnemy(Vector3 position, Quaternion rotation)
    {
        if (!isInitialized && !Initialize(enemyPrefab, initialPoolSize))
            return null;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeInHierarchy)
                return ActivateEnemy(enemies[i], position, rotation);
        }

        if (canExpand)
            return ActivateEnemy(CreateEnemy(), position, rotation);

        return null;
    }

    EnemyController ActivateEnemy(EnemyController enemy, Vector3 position, Quaternion rotation)
    {
        enemy.transform.SetParent(transform);
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(EnemyController enemy)
    {
        if (enemy == null)
            return;

        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(transform);
    }
}
