using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null)
            return;

        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x, maxBounds.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y, maxBounds.y);
        mainCamera.transform.position = cameraPosition;
    }
}
