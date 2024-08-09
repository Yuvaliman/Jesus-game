using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public Vector3 Offset; 
    public float followSpeed = 5f;  // Speed at which the orb follows the player
    public float sineFrequency = 1f;  // Frequency of the sine wave
    public float sineAmplitude = 0.1f;  // Amplitude of the sine wave

    private Transform playerTransform;  // Reference to the player's transform
    private Vector3 currentOffset;  // Current offset based on player's facing direction
    private Vector3 basePosition;  // Base position to apply sine wave effect

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            UpdateOffset();
            FollowPlayerWithOffset();
        }
    }

    void UpdateOffset()
    {
        bool isPlayerFlipped = player.GetComponent<SpriteRenderer>().flipX;
        // Determine the current offset based on player's facing direction

        currentOffset = isPlayerFlipped ? new Vector3(Offset.x * -1, Offset.y, Offset.z) : Offset;
    }

    void FollowPlayerWithOffset()
    {
        // Calculate the target position with the offset
        basePosition = playerTransform.position + currentOffset;

        // Move the orb towards the target position
        transform.position = Vector3.MoveTowards(transform.position, basePosition, followSpeed * Time.deltaTime);

        // Apply subtle sine wave movement
        transform.position = new Vector3(transform.position.x, basePosition.y + Mathf.Sin(Time.time * sineFrequency) * sineAmplitude, transform.position.z);
    }
}
