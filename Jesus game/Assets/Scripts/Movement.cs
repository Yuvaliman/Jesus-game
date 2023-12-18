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
    public float dashSpeed = 200f;
    private float nextDashTime = 0f;
    public float lastMoveDirectionX = 0f;
    public float lastMoveDirectionY = 0f;
    public float dashCooldown = 0f;

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

        // Update player rotation
        UpdatePlayerRotation();

        UpdateLastMoveDirection(horizontal, vertical);

        // Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Check the last movement direction
            if (lastMoveDirectionX != 0f || lastMoveDirectionY != 0f)
            {
                // Move the player in the same way they went before with dash speed boost
                StartCoroutine(DashCoroutine());
            }
        }
    }

    IEnumerator DashCoroutine()
    {
        // Check if enough time has passed since the last dash
        if (Time.time >= nextDashTime)
        {
            // Boost the player's speed
            body.velocity = new Vector2(lastMoveDirectionX * dashSpeed, lastMoveDirectionY * dashSpeed);

            // Set the next dash time based on the cooldown
            nextDashTime = Time.time + dashCooldown;

            // Wait for a moment, then call StopDashing function
            yield return new WaitForSeconds(0.1f);

            // Function to stop dashing
            body.velocity = Vector2.zero;
        }
    }

    void UpdateLastMoveDirection(float h, float v)
    {
        lastMoveDirectionX = Mathf.Clamp(h, -1f, 1f);
        lastMoveDirectionY = Mathf.Clamp(v, -1f, 1f);
    }

    void UpdateAnimation()
    {
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("IsWalking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsRunning", true);
                MoveSpeed = 7f;
            }
            else
            {
                anim.SetBool("IsRunning", false);
                MoveSpeed = 5f;
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    void UpdatePlayerRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse is on the right or left side of the screen
        if (mousePosition.x > transform.position.x)
        {
            // Mouse is on the right, face right
            spriteRenderer.flipX = false;
        }
        else
        {
            // Mouse is on the left, face left
            spriteRenderer.flipX = true;
        }
    }
}
