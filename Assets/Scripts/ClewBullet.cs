using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClewBullet : MonoBehaviour
{

    private string _blockTag = "Block";
    private string _enemyTag = "Enemy";
    private string _trapTag = "Trap";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _blockTag || collision.gameObject.tag == _enemyTag
            || collision.gameObject.tag == _trapTag )
        {
            Destroy(gameObject);
        }
    }
}
