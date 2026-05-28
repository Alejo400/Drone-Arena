using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerConfig",
    menuName = "Drone Arena/Player/PlayerConfig"
)]

public class PlayerConfig : ScriptableObject
{
    [Header("Health")]
    [SerializeField] float maxHealth;
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [Header("Aim")]
    [SerializeField] float rotationSpeed;
    [Header("Shooting")]
    [SerializeField] float fireRate;
    [SerializeField] float projectileDamage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifeTime;

    public float MaxHealth => maxHealth;
    public float MoveSpeed => moveSpeed;
    public float RotationSpeed => rotationSpeed;
    public float FireRate => fireRate;
    public float ProjectileDamage => projectileDamage;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileLifeTime => projectileLifeTime;
}
