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
    private Collider2D _thisCollider;

    void Start()
    {
        _stopCollider1 = Stopper1.GetComponent<Collider2D>();
        _stopCollider2 = Stopper2.GetComponent<Collider2D>();
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
            ArrowGun.StartFire();

            _stopCollider1.enabled = true;
            _stopCollider2.enabled = true;
            ArrowGun.IsCharacterEntered = true;
        }
    }
}
