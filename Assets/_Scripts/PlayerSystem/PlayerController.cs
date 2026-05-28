using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAimController))]
[RequireComponent(typeof(PlayerShooter))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] Transform aimRoot, firePoint;

    PlayerMovement playerMovement;
    PlayerAimController playerAimController;
    PlayerShooter playerShooter;
    PlayerHealth playerHealth;

    void OnEnable()
    {
        GameEventSystem.OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        GameEventSystem.OnPlayerDied -= HandlePlayerDied;
    }

    void Awake()
    {

        if(playerConfig == null)
        {
            Debug.LogError("Player config is required");
            return;
        } 
        if(firePoint == null)
        {
            Debug.LogError("FirePoint is required");
            return;
        }  

        playerMovement = GetComponent<PlayerMovement>();
        playerAimController = GetComponent<PlayerAimController>();
        playerShooter = GetComponent<PlayerShooter>();
        playerHealth = GetComponent<PlayerHealth>();

        if(aimRoot == null)
            aimRoot = transform;

        playerMovement.Initialize(playerConfig);
        playerAimController.Initialize(playerConfig, aimRoot);
        playerShooter.Initialize(playerConfig, firePoint);
        playerHealth.Initialize(playerConfig);
    }

    void HandlePlayerDied()
    {
        if (playerMovement != null)
            playerMovement.SetMovementEnabled(false);

        if (playerAimController != null)
            playerAimController.SetAimEnabled(false);

        if (playerShooter != null)
            playerShooter.SetShootingEnabled(false);
    }
}
