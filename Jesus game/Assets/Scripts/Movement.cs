using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private float horizontal;
    private float vertical;

    public float MoveSpeed = 5f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Assign the SpriteRenderer component
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
        if (horizontal > 0)
        {
            // Moving right, flip the player to face right
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            // Moving left, flip the player to face left
            spriteRenderer.flipX = true;
        }
    }
}
