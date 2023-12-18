using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public SpriteRenderer spriteRenderer;

    private float horizontal;
    private float vertical;

    public float MoveSpeed = 5f;

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
