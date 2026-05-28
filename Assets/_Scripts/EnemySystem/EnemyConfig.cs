using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyConfig",
    menuName = "Drone Arena/Enemies/EnemyConfig"
)]
public class EnemyConfig : ScriptableObject
{
    [Header("Health")]
    [SerializeField] float maxHealth;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float stoppingDistance;

    [Header("Damage")]
    [SerializeField] float contactDamagePerSecond;

    [Header("Score")]
    [SerializeField] int pointsOnDeath;

    public float MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float StoppingDistance => stoppingDistance;
    public float ContactDamagePerSecond => contactDamagePerSecond;
    public int PointsOnDeath => pointsOnDeath;
}
