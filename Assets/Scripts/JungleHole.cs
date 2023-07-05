using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JungleHole : Trap
{
    public Sprite Hole;

    private string _characterTag = "Character";
    private string _enemyTag = "Enemy";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _characterTag)
        {
            GetComponent<SpriteRenderer>().sprite = Hole;
        }

        if (collision.gameObject.tag == _enemyTag)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
