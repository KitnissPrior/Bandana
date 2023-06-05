using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsStarter : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject Stopper1;
    public GameObject Stopper2;
    public Bullet Stone;

    private string _characterTag = "Character";
    private Collider2D _stopCollider1;
    private Collider2D _stopCollider2;

    void Start()
    {
        _stopCollider1 = Stopper1.GetComponent<Collider2D>();
        _stopCollider2 = Stopper2.GetComponent<Collider2D>();
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
            ArrowGun.StartFire();

            _stopCollider1.enabled = true;
            _stopCollider2.enabled = true;
            ArrowGun.IsCharacterEntered = true;
        }
    }
}
