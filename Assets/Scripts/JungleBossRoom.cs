using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleBossRoom : MonoBehaviour
{
    public ShootingBoss Boss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterBullet>(out CharacterBullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            Boss.IsCharacterEntered = true;
            Destroy(gameObject);
        }
    }
}
