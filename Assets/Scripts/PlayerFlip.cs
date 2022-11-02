using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public PlayerController PlayerController;

    public void Flip()
    {
        PlayerController.FacingRight = !PlayerController.FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        PlayerController.transform.localScale = Scaler;
    }

}
