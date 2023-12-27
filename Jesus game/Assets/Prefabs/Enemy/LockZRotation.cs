using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZRotation : MonoBehaviour
{
    private void Update()
    {
        // Get the current rotation
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Lock the Z-axis rotation to a specific angle
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
    }
}
