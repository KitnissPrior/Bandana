using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    public PlayerController PlayerController;
    public float NormalSpeed;

    public void Run(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        PlayerController.PlayerRb.velocity = new Vector2(inputVec.x, inputVec.y);

        PlayerController.Speed = NormalSpeed * inputVec.x;
    }

}