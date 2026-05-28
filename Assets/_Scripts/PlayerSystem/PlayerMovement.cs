using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    PlayerConfig playerConfig;
    Rigidbody2D rb;
    Vector2 moveInput;
    bool canMove = true;

    public void Initialize(PlayerConfig config)
    {
        playerConfig = config;
        SetMovementEnabled(true);
    }

    public void SetMovementEnabled(bool isEnabled)
    {
        canMove = isEnabled;

        if (canMove)
            return;

        moveInput = Vector2.zero;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    void Awake()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            moveInput = Vector2.zero;
            return;
        }

        GetMovementInput();
    }
    void FixedUpdate()
    {
        if (!canMove)
            return;

        Move();
    }
    void GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(horizontal, vertical).normalized;
    }
    void Move()
    {
        if (playerConfig == null || rb == null)
            return;

        Vector2 targetPosition = rb.position + moveInput * playerConfig.MoveSpeed 
                                * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }
}
