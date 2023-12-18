using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    private float horizontal;
    private float vertical;

    public float MoveSpeed = 5f;

    // Dash variables
    public float dashCooldown = 1f;
    private float nextDashTime = 0f;
    public float dashDistance = 10f;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Update animation parameters
        UpdateAnimation();

        // Update player movement
        body.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);

        // Face mouse
        FaceMouse();

        // Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        if (Time.time >= nextDashTime)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Calculate the destination point based on a fixed distance from the player
            Vector2 dashDestination = (mousePos - transform.position).normalized * dashDistance;

            // Save the initial position for reference
            Vector2 initialPosition = transform.position;

            nextDashTime = Time.time + dashCooldown;

            // Dash duration based on distance (adjust as necessary)
            float dashDuration = 0.2f;
            float startTime = Time.time;

            while (Time.time < startTime + dashDuration)
            {
                // Move the player towards the dash destination
                float journeyFraction = (Time.time - startTime) / dashDuration;
                transform.position = Vector2.Lerp(initialPosition, initialPosition + dashDestination, journeyFraction);
                yield return null;
            }

            transform.position = initialPosition + dashDestination; // Ensure the player reaches the destination exactly
        }
    }

    void UpdateAnimation()
    {
        bool isMoving = horizontal != 0 || vertical != 0;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        anim.SetBool("IsWalking", isMoving);

        if (isMoving)
        {
            if (isRunning)
            {
                anim.SetBool("IsRunning", true);
                MoveSpeed = 7f; // Set running speed
            }
            else
            {
                anim.SetBool("IsRunning", false);
                MoveSpeed = 5f; // Set walking speed
            }
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void FaceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Flip the sprite based on mouse position
        if (mousePos.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else
        {
            spriteRenderer.flipX = true; // Face left
        }
    }
}
