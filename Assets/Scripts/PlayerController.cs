using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRb;

    public float Speed;
    public float MoveInput;

    public bool FacingRight;
    public bool IsGrounded;
    public Transform FeetPos;
    public float CheckRadius;
    public LayerMask WhatIsGround;

    private void Start()
    {
        Speed = 0f;
        IsGrounded = true;
        FacingRight = true;
    }

    public void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(FeetPos.position, CheckRadius, WhatIsGround);

        PlayerRb.velocity = new Vector2(Speed, PlayerRb.velocity.y);
        if ((!FacingRight && MoveInput > 0) || (FacingRight && MoveInput < 0))
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
        PlayerRb.BroadcastMessage("Run", input);
    }

}