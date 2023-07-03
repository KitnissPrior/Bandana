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
    public bool IsFrozen;

    private Vector2 _moveInput;

    private void Start()
    {
        PlayerVelocity = new Vector2();
        FacingRight = true;
        IsFrozen = false;
    }

    public void Update()
    {
        PlayerRb.velocity = PlayerVelocity;
        if (((!FacingRight && _moveInput.x > 0) || (FacingRight && _moveInput.x < 0)) && !IsFrozen)
        {
            PlayerRb.BroadcastMessage("Flip");
        }
    }

    public void FixedUpdate()
    {
        if (IsFrozen) PlayerVelocity = new Vector2(0, 0);

        PlayerRb.MovePosition(PlayerRb.position + PlayerVelocity * Time.fixedDeltaTime);
    }

    public void OnJump()
    {
        PlayerRb.BroadcastMessage("Jump");
    }

    public void OnRun(InputValue input)
    {
        _moveInput = input.Get<Vector2>();
        PlayerRb.BroadcastMessage("Run", input);
    }

}