using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The target (player) the camera will follow
    public Vector3 offset;         // Offset position relative to the target
    public float smoothSpeed = 0.125f; // Smoothness factor for movement

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned to the camera.");
            return;
        }

        // Calculate the desired position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Optionally, ensure the camera is always looking at the target
        // transform.LookAt(target); // Uncomment if you want the camera to rotate toward the target
    }
}
