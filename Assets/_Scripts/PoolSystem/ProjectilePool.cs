using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int initialPoolSize;
    bool canExpand = true;

    readonly List<Projectile> projectiles = new();
    void Awake()
    {
        CreateInitialPool();
    }

    void CreateInitialPool()
    {
        if(projectilePrefab == null)
        {
            Debug.Log("Projectile Prefab is required");
            return;
        }
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateProjectile();
        }
    }
    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.SetPool(this);
        projectile.gameObject.SetActive(false);

        projectiles.Add(projectile);

        return projectile;
    }
    public Projectile GetProjectile()
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            if (!projectiles[i].gameObject.activeInHierarchy)
            {
                projectiles[i].gameObject.SetActive(true);
                return projectiles[i];
            }
        }

        if (canExpand)
        {
            Projectile newProjectile = CreateProjectile();
            newProjectile.gameObject.SetActive(true);
            return newProjectile;
        }

        return null;
    }
    public void ReturnProjectile(Projectile projectile)
    {
        if (projectile == null)
        {
            return;
        }

        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
    }
}
