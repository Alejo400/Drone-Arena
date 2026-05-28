using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    EnemyConfig enemyConfig;
    Rigidbody2D rb;
    Transform playerTarget;

    public void Initialize(EnemyConfig config, Transform target)
    {

        if(config == null || target == null)
        {
            Debug.LogError("EnemyConfig and Target are required", this);
            enabled = false;
            return;
        }

        enemyConfig = config;
        playerTarget = target;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (enemyConfig == null || playerTarget == null)
            return;

        Vector2 toPlayer = (Vector2)playerTarget.position - rb.position;
        float distanceToPlayer = toPlayer.magnitude;

        if (distanceToPlayer <= enemyConfig.StoppingDistance)
            return;

        Vector2 direction = toPlayer.normalized;
        float maxStep = enemyConfig.MoveSpeed * Time.fixedDeltaTime;
        float allowedStep = distanceToPlayer - enemyConfig.StoppingDistance;
        float moveStep = Mathf.Min(maxStep, allowedStep);

        Vector2 targetPosition = rb.position + direction * moveStep;
        rb.MovePosition(targetPosition);
    }
}
