using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JungleHole : Trap
{
    public Sprite Hole;

    private string _characterTag = "Character";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == _characterTag)
        {
            GetComponent<SpriteRenderer>().sprite = Hole;
        }
    }
}
