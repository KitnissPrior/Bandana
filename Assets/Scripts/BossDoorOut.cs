using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorOut : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if (character.HasKey)
            {
                Destroy(gameObject);
            }
        }
    }
}
