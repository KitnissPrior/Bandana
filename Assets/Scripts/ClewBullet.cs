using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClewBullet : MonoBehaviour
{

    private string _blockTag = "Block";
    private string _trapTag = "Trap";
    private string _enemyTag = "Enemy";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _blockTag || collision.gameObject.tag == _trapTag 
            || collision.gameObject.TryGetComponent<Boss>(out Boss boss))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == _enemyTag)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
