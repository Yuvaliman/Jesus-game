using UnityEngine;

public class OrbFloat : MonoBehaviour
{
    public Transform playerTransform;
    public SpriteRenderer PlayerSpriteRenderer;
    public Transform orbTransform;
    public float horizontalMovementSpeed = 5.0f;
    public float verticalMovementSpeed = 2.0f;
    public float lerpFactor = 0.1f;
    public Vector3 offset = new Vector3(-0.45f, 0.25f, 0.0f);
    public float closeDistanceThreshold = 1.5f;
    public float amplitude = 0.01f;

    void Update()
    {
        if (playerTransform != null && orbTransform != null)
        {
            float flipMultiplier = PlayerSpriteRenderer.flipX ? -1f : 1f;
            Vector3 targetPosition = playerTransform.position + new Vector3(offset.x * flipMultiplier, offset.y, offset.z);
            Vector3 newPosition = Vector3.Lerp(orbTransform.position, targetPosition, lerpFactor * Time.deltaTime * horizontalMovementSpeed);

            float distanceToPlayer = Vector3.Distance(orbTransform.position, playerTransform.position);
            bool isClose = distanceToPlayer <= closeDistanceThreshold;

            if (isClose)
            {
                float verticalOffset = Mathf.Sin(Time.time * verticalMovementSpeed) * amplitude; // Adjust amplitude as needed
                newPosition.y += verticalOffset;
            }

            orbTransform.position = newPosition;
        }
    }
}
