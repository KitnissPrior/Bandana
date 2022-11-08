using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRb;

    public Vector2 PlayerVelocity;
    public bool FacingRight;
    public bool IsGrounded;
    public Transform FeetPos;
    public float CheckRadius;
    public LayerMask WhatIsGround;

    private Vector2 moveInput;

    private void Start()
    {
        PlayerVelocity = new Vector2();
        FacingRight = true;
    }

    public void Update()
    {
        //IsGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, WhatIsGround);

        PlayerRb.velocity = PlayerVelocity;
        if ((!FacingRight && moveInput.x > 0) || (FacingRight && moveInput.x < 0))
        {
            PlayerRb.BroadcastMessage("Flip");
        }
    }

    public void FixedUpdate()
    {
        PlayerRb.MovePosition(PlayerRb.position + PlayerVelocity * Time.fixedDeltaTime);
    }

    public void OnJump()
    {
        PlayerRb.BroadcastMessage("Jump");
    }

    public void OnRun(InputValue input)
    {
        moveInput = input.Get<Vector2>();

        PlayerRb.BroadcastMessage("Run", input);
    }

}