using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerController PlayerController;

    public float JumpForce = 7f;

    public void Jump()
    {
        if (PlayerController.IsGrounded)
        {
            PlayerController.PlayerRb.velocity = Vector2.up * JumpForce;
        }
    }
}