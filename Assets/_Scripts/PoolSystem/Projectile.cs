using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Projectile : MonoBehaviour
{
    ProjectilePool projectilePool;
    Rigidbody2D rb;
    Coroutine lifeTimeCoroutine;

    float damage;
    bool hasHit;

    public void SetPool(ProjectilePool pool)
    {
        projectilePool = pool;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Launches the projectile and schedules its return to the pool.
    /// </summary>
    /// <param name="direction">Direction in which the projectile moves.</param>
    /// <param name="speed">Movement speed of the projectile.</param>
    /// <param name="damageAmount">Damage when the projectile hits.</param>
    /// <param name="lifeTime">Time before the projectile returns to the pool.</param>
    public void Launch(Vector2 direction, float speed, float damageAmount, float lifeTime)
    {
        hasHit = false;
        damage = damageAmount;

        direction.Normalize();

        rb.linearVelocity = direction * speed;

        if (lifeTimeCoroutine != null)
        {
            StopCoroutine(lifeTimeCoroutine);
        }

        lifeTimeCoroutine = StartCoroutine(ReturnAfterLifeTime(lifeTime));
    }
    private IEnumerator ReturnAfterLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        ReturnToPool();
    }
    //It returns if it hits or lifetime end.
    private void ReturnToPool()
    {
        if (lifeTimeCoroutine != null)
        {
            StopCoroutine(lifeTimeCoroutine);
            lifeTimeCoroutine = null;
        }

        rb.linearVelocity = Vector2.zero;
        damage = 0f;
        hasHit = false;

        if (projectilePool != null)
            projectilePool.ReturnProjectile(this);
        else
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
            return;

        hasHit = true;
        enemyHealth.TakeDamage(damage);

        ReturnToPool();
    }
    private void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (lifeTimeCoroutine != null)
        {
            StopCoroutine(lifeTimeCoroutine);
            lifeTimeCoroutine = null;
        }
    }
}
