using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField] float angleOffset = 0f;

    PlayerConfig playerConfig;
    Transform aimRoot;
    Camera mainCamera;
    bool canAim = true;

    public void Initialize(PlayerConfig config, Transform aimRootRef)
    {

        if (config == null)
        {
            Debug.LogError("PlayerConfig is required", this);
            SetAimEnabled(false);
            return;
        }

        if (aimRootRef == null)
        {
            Debug.LogError("AimRoot is required", this);
            SetAimEnabled(false);
            return;
        }

        if(Camera.main == null)
        {
            Debug.LogError("Camera is necessary", this);
            SetAimEnabled(false);
            return;
        }

        playerConfig = config;
        aimRoot = aimRootRef;
        mainCamera = Camera.main;
        SetAimEnabled(true);
    }

    public void SetAimEnabled(bool isEnabled)
    {
        canAim = isEnabled;
    }
    void Update()
    {
        if (!canAim)
            return;

        RotateTowardsMouse();
    }
    //Allow rotate the player based on mouse position-movement
    void RotateTowardsMouse()
    {
        if (playerConfig == null || aimRoot == null || mainCamera == null)
            return;

        Vector3 mouseScreenPosition = Input.mousePosition;

        float distanceFromCameraToPlayer = Mathf.Abs(
            mainCamera.transform.position.z - aimRoot.position.z
        );

        mouseScreenPosition.z = distanceFromCameraToPlayer;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 direction = mouseWorldPosition - aimRoot.position;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle += angleOffset;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        aimRoot.rotation = Quaternion.RotateTowards(
            aimRoot.rotation,
            targetRotation,
            playerConfig.RotationSpeed * Time.deltaTime
        );
    }
}
