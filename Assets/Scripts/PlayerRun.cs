using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    public PlayerController PlayerController;

    private float _moveSpeed;

    public void Run(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        PlayerController.PlayerVelocity = _moveSpeed * inputVec;
    }

    internal void Initialize(float speed)
    {
        _moveSpeed = speed;
    }
}