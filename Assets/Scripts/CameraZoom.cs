using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera;
    public BoxCollider2D levelBoundsCollider;
    public float zoomOutSize = 20f;
    public float zoomSpeed = 2f;
    public Bounds levelBounds;

    private float originalSize;
    private Vector3 originalPosition; // Store the original position
    private bool isZoomedOut = false;

    
    // Start is called before the first frame update
    void Start()
    {
        levelBounds = levelBoundsCollider.bounds;
        if (mainCamera.orthographic)
        {
            originalSize = mainCamera.orthographicSize;
        }
        else
        {
            originalSize = mainCamera.fieldOfView;
        }
        originalPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleZoom()
    {
        if (isZoomedOut)
        {
            // Zoom back to the original view
            StartCoroutine(SmoothZoom(originalSize, originalPosition));
            isZoomedOut = false;
        }
        else
        {
            // Zoom out to show the whole level
            Vector3 targetPosition = levelBounds.center;
            float targetSize = CalculateZoomOutSize();

            StartCoroutine(SmoothZoom(targetSize, targetPosition));
            isZoomedOut = true;
        }
    }
    private float CalculateZoomOutSize()
    {
        if (mainCamera.orthographic)
        {
            return Mathf.Max(levelBounds.size.x / mainCamera.aspect, levelBounds.size.y) / 2f;
        }
        else
        {
            float distance = Vector3.Distance(mainCamera.transform.position, levelBounds.center);
            float angle = Mathf.Atan(levelBounds.size.y / distance) * Mathf.Rad2Deg * 2f;
            return Mathf.Max(angle, mainCamera.fieldOfView);
        }
    }

    private System.Collections.IEnumerator SmoothZoom(float targetSize, Vector3 targetPosition)
    {
        float currentSize = mainCamera.orthographic ? mainCamera.orthographicSize : mainCamera.fieldOfView;
        Vector3 currentPosition = mainCamera.transform.position;

        while (Mathf.Abs(currentSize - targetSize) > 0.01f)
        {
            currentSize = Mathf.Lerp(currentSize, targetSize, Time.deltaTime * zoomSpeed);
            currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * zoomSpeed);

            if (mainCamera.orthographic)
            {
                mainCamera.orthographicSize = currentSize;
            }
            else
            {
                mainCamera.fieldOfView = currentSize;
            }
            mainCamera.transform.position = currentPosition;

            yield return null;
        }

        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize = targetSize;
        }
        else
        {
            mainCamera.fieldOfView = targetSize;
        }
        mainCamera.transform.position = targetPosition;
    }

}
