using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public Vector3 CamOffset;
    public float smoothSpeed = 5f;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    void LateUpdate()
    {
        if (Player == null) return;

        Vector3 desiredPosition = Player.position + CamOffset;

        float clampedX = Mathf.Clamp(
            desiredPosition.x,
            minBounds.x + camHalfWidth,
            maxBounds.x - camHalfWidth
        );

        float clampedY = Mathf.Clamp(
            desiredPosition.y,
            minBounds.y + camHalfHeight,
            maxBounds.y - camHalfHeight
        );

        Vector3 finalPosition = new Vector3(clampedX, clampedY, CamOffset.z);

        transform.position = Vector3.Lerp(
            transform.position,
            finalPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}