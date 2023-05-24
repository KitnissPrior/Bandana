using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableItem : MonoBehaviour
{
    public string DestroyingTag = "Character";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == DestroyingTag)
        {
            Destroy(gameObject);
        }
    }


}
