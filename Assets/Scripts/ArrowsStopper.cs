using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsStopper : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject Starter1;
    public GameObject Starter2;
    public Bullet Stone;

    private string _characterTag = "Character";
    private Collider2D _startCollider1;
    private Collider2D _startCollider2;

    void Start()
    {
        _startCollider1 = Starter1.GetComponent<Collider2D>();
        _startCollider2 = Starter2.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == _characterTag)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            ArrowGun.StopFire();

            _startCollider1.enabled = true;
            _startCollider2.enabled = true;
            ArrowGun.IsCharacterEntered = false;
        }
    }
}
