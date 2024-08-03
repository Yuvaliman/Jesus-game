using UnityEngine;

public class DepthSystem : MonoBehaviour
{
    [SerializeField] private float depthMultiplier = -0.1f; // Multiplier for calculating depth based on y-value
    [SerializeField] private float minDepth = -10f; // Minimum z-value
    [SerializeField] private float maxDepth = 10f;  // Maximum z-value

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Ensure there is a camera tagged as 'MainCamera' in the scene.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            UpdateDepth(transform);
        }
    }

    void UpdateDepth(Transform objTransform)
    {
        // Convert world position to viewport position (normalized screen coordinates)
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(objTransform.position);

        // Calculate the new z-value based on the viewport y-value
        float normalizedY = viewportPosition.y;
        Vector3 position = objTransform.position;
        position.z = Mathf.Clamp(normalizedY * depthMultiplier, minDepth, maxDepth);
        objTransform.position = position;

        // Recursively update depth for all child objects
        foreach (Transform child in objTransform)
        {
            UpdateDepth(child);
        }
    }
}
