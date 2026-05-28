using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemySpawnConfig",
    menuName = "Drone Arena/Spawning/EnemySpawnConfig"
)]
public class EnemySpawnConfig : ScriptableObject
{
    [Header("Spawn")]
    [SerializeField] float initialSpawnInterval;
    [SerializeField] int maxEnemiesAlive;
    [SerializeField] float minimumDistanceFromPlayer;

    [Header("Difficulty Ramp")]
    [SerializeField] float spawnIntervalReductionEverySeconds;
    [SerializeField] float spawnIntervalReductionAmount;
    [SerializeField] float minimumSpawnInterval;

    public float InitialSpawnInterval => initialSpawnInterval;
    public int MaxEnemiesAlive => maxEnemiesAlive;
    public float MinimumDistanceFromPlayer => minimumDistanceFromPlayer;

    public float SpawnIntervalReductionEverySeconds => spawnIntervalReductionEverySeconds;
    public float SpawnIntervalReductionAmount => spawnIntervalReductionAmount;
    public float MinimumSpawnInterval => minimumSpawnInterval;
}