using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float normalSpeed;
    
    public float jumpForce;
    public float moveInput;

    

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private void Start()
    {
        speed = 0f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2( speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    public void OnJumpButtonDown()
    {
        if (isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        } 
    }

    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;
        }
    }
    
    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
        }
    }

    public void OnButtonUp()
    {
        speed = 0f;
    }

    public void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
