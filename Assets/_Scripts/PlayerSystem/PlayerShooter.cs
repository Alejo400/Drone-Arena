using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] ProjectilePool projectilePool;
    PlayerConfig playerConfig;
    Transform firePoint;
    float nextFireTime;
    bool canShoot = true;
    public void Initialize(PlayerConfig config, Transform firePointRef)
    {
        if(config == null)
        {
            Debug.LogError("PlayerConfig is required", this);
            SetShootingEnabled(false);
            return;
        }

        if (firePointRef == null)
        {
            Debug.LogError("Its neccesary FirePoint", this);
            SetShootingEnabled(false);
            return;
        }

        if (projectilePool == null)
        {
            Debug.LogError("Its neccesary Pool", this);
            SetShootingEnabled(false);
            return;
        }

        playerConfig = config;
        firePoint = firePointRef;
        SetShootingEnabled(true);
    }

    public void SetShootingEnabled(bool isEnabled)
    {
        canShoot = isEnabled;
    }
    void Update()
    {
        if (!canShoot)
            return;

        HandleShootingInput();
    }
    
    //Verify and validate shoot + apply cooldown
    void HandleShootingInput()
    {
        if (!Input.GetMouseButton(0))
            return;
        
        if(Time.time < nextFireTime)
            return;
        
        Shoot();
        float fireCooldown = 1f / playerConfig.FireRate;
        nextFireTime = Time.time + fireCooldown;
    }

    private void Shoot()
    {
        Projectile projectile = projectilePool.GetProjectile();

        if (projectile == null)
            return;

        projectile.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);

        projectile.Launch(
            firePoint.right,
            playerConfig.ProjectileSpeed,
            playerConfig.ProjectileDamage,
            playerConfig.ProjectileLifeTime
        );
    }
}
