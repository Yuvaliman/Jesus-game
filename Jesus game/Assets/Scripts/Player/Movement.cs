using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    private float horizontal;
    private float vertical;

    public float MoveSpeed = 20f;

    // Dash variables
    public float dashCooldown = 1f;
    private float nextDashTime = 0f;
    public float dashDistance = 10f;

    public Image staminaBarForegroundImage;
    private float staminaRunningDecreaseRate = .15f;
    private float staminaDashDecreaseRate = 0.25f;

    [SerializeField]
    private Transform feetParticleFX;

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
        if (Input.GetKeyDown(KeyCode.Space) && staminaBarForegroundImage.fillAmount > 0.01f)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        if (Time.time >= nextDashTime && staminaBarForegroundImage.fillAmount > 0.25f)
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

            staminaBarForegroundImage.fillAmount -= staminaDashDecreaseRate;
        }
    }

    void UpdateAnimation()
    {
        bool isMoving = horizontal != 0 || vertical != 0;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        anim.SetBool("IsWalking", isMoving);

        if (isMoving)
        {
            if (isRunning && staminaBarForegroundImage.fillAmount > 0.01f)
            {
                anim.SetBool("IsRunning", true);
                MoveSpeed = 20f;

                staminaBarForegroundImage.fillAmount -= staminaRunningDecreaseRate * Time.deltaTime;
            }
            else
            {
                anim.SetBool("IsRunning", false);
                MoveSpeed = 15f;
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

        // Flip the player based on mouse position
        if (mousePos.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Face right
            boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
            boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
            feetParticleFX.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation for right-facing
        }
        else
        {
            spriteRenderer.flipX = true; // Face left
            boxCollider.offset = new Vector2(-Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
            feetParticleFX.rotation = Quaternion.Euler(0, 180, 0); // Rotate for left-facing
        }
    }
}
