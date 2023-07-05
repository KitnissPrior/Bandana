using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGunController : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject AnotherController;

    private string _characterTag = "Character";
    private int _timeToLeave = 1;

    private IEnumerator StartControllers()
    {
        yield return new WaitForSeconds(_timeToLeave);

        gameObject.GetComponent<Collider2D>().enabled = true;
        AnotherController.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterBullet>(out CharacterBullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == _characterTag)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(StartControllers());
        }
    }
}
