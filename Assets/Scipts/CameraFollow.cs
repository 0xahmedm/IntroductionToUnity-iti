using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Player
    public Vector3 offset;        // Camera offset
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
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

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

        Vector3 finalPosition = new Vector3(clampedX, clampedY, offset.z);

        transform.position = Vector3.Lerp(
            transform.position,
            finalPosition,
            smoothSpeed * Time.deltaTime
        );
    }

    /* void LateUpdate()
     {
         if (target == null) return;

         Vector3 desiredPosition = new Vector3(target.position.x,
             transform.position.y, transform.position.z);

         desiredPosition = target.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(
             transform.position,
             desiredPosition,
             smoothSpeed * Time.deltaTime
         );

         transform.position = smoothedPosition;
     }*/
}
