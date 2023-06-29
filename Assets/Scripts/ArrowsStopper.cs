using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsStopper : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject Starter1;
    public GameObject Starter2;
    public Bullet Stone;

    private Collider2D _startCollider1;
    private Collider2D _startCollider2;
    private Collider2D _thisCollider;

    void Start()
    {
        _startCollider1 = Starter1.GetComponent<Collider2D>();
        _startCollider2 = Starter2.GetComponent<Collider2D>();
        _thisCollider = gameObject.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if (character.Shield.IsActive)
            {
                Physics2D.IgnoreCollision(character.Shield.GetComponent<Collider2D>(), _thisCollider);
            }

            _thisCollider.enabled = false;
            ArrowGun.StopFire();

            _startCollider1.enabled = true;
            _startCollider2.enabled = true;
            ArrowGun.IsCharacterEntered = false;
        }
    }
}
