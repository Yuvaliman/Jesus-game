using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 5f; // Adjust this value to control the smoothness of the camera movement

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
