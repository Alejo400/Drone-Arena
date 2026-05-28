using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] EnemySpawnConfig spawnConfig;
    [SerializeField] EnemyConfig enemyConfig;

    [Header("References")]
    [SerializeField] EnemyController enemyPrefab;
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform player;
    [SerializeField] List<Transform> spawnPoints = new();

    int currentEnemiesAlive;
    float currentSpawnInterval;
    Coroutine spawnCoroutine, difficultyCoroutine;

    void OnEnable()
    {
        GameEventSystem.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        GameEventSystem.OnEnemyKilled -= HandleEnemyKilled;

        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);

        if (difficultyCoroutine != null)
            StopCoroutine(difficultyCoroutine);
    }

    void Start()
    {
        if (spawnConfig == null)
        {
            Debug.LogError("SpawnConfig is required", this);
            return;
        }

        if (enemyConfig == null)
        {
            Debug.LogError("EnemyConfig is required", this);
            return;
        }

        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyPrefab is required", this);
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player Transform is required", this);
            return;
        }

        if (enemyPool == null)
            enemyPool = GetComponent<EnemyPool>();

        if (enemyPool == null)
            enemyPool = gameObject.AddComponent<EnemyPool>();

        if (!enemyPool.Initialize(enemyPrefab, spawnConfig.MaxEnemiesAlive))
            return;

        currentSpawnInterval = spawnConfig.InitialSpawnInterval;

        spawnCoroutine = StartCoroutine(SpawnLoop());
        difficultyCoroutine = StartCoroutine(DifficultyRampLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnInterval);
            TrySpawnEnemy();
        }
    }
    //Spawn of Enemy if we have a valid spawn point to instantiate
    void TrySpawnEnemy()
    {
        if (currentEnemiesAlive >= spawnConfig.MaxEnemiesAlive)
            return;

        Transform spawnPoint = GetValidSpawnPoint();

        if (spawnPoint == null)
            return;

        EnemyController enemy = enemyPool.GetEnemy(spawnPoint.position, spawnPoint.rotation);

        if (enemy == null)
            return;

        enemy.Initialize(player, enemyConfig);

        currentEnemiesAlive++;
    }

    /*Find a valid spawn point based on distancePlayer, determine if there is a valid spawn point,
    choose a random valid spawn point and return to TrySpawnEnemy()
    */
    Transform GetValidSpawnPoint()
    {
        List<Transform> validSpawnPoints = new();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            if (spawnPoint == null)
                continue;

            float distanceToPlayer = Vector2.Distance(
                spawnPoint.position,
                player.position
            );

            if (distanceToPlayer >= spawnConfig.MinimumDistanceFromPlayer)
                validSpawnPoints.Add(spawnPoint);
        }

        if (validSpawnPoints.Count == 0)
            return null;

        int randomIndex = Random.Range(0, validSpawnPoints.Count);
        return validSpawnPoints[randomIndex];
    }

    //Every time reduce invertal enemy spawn to increase difficulty based on spawn configuration
    IEnumerator DifficultyRampLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnConfig.SpawnIntervalReductionEverySeconds);

            currentSpawnInterval -= spawnConfig.SpawnIntervalReductionAmount;

            if (currentSpawnInterval < spawnConfig.MinimumSpawnInterval)
                currentSpawnInterval = spawnConfig.MinimumSpawnInterval;
        }
    }

    void HandleEnemyKilled(int points)
    {
        currentEnemiesAlive--;

        if (currentEnemiesAlive < 0)
            currentEnemiesAlive = 0;
    }
}
