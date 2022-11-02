using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRb;

    public float Speed;
    public bool FacingRight;
    public bool IsGrounded;
    public Transform FeetPos;
    public float CheckRadius;
    public LayerMask WhatIsGround;

    private float moveInput;

    private void Start()
    {
        Speed = 0f;
        FacingRight = true;
    }

    public void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, WhatIsGround);

        PlayerRb.velocity = new Vector2(Speed, PlayerRb.velocity.y);
        if ((!FacingRight && moveInput > 0) || (FacingRight && moveInput < 0))
        {
            PlayerRb.BroadcastMessage("Flip");
        }
    }

    public void OnJump()
    {
        PlayerRb.BroadcastMessage("Jump");
    }

    public void OnRun(InputValue input)
    {
        moveInput = input.Get<Vector2>().x;

        PlayerRb.BroadcastMessage("Run", input);
    }

}