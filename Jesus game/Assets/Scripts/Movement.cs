using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator anim;

    private float horizontal;
    private float vertical;

    public float MoveSpeed = 5.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("IsWalking", true);
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        body.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);
    }
}
