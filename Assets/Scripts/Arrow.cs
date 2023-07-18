using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    public CommonData CommonData;

    private string _characterTag = "Character";

    private void CheckIfCharacterCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Health>(out Health health) && collidedObject.tag == _characterTag)
        {
            if (health != Parent)
            {
                Character character = collidedObject.GetComponent<Character>();

                if (character.Shield.IsActive)
                {
                    Physics2D.IgnoreCollision(character.Shield.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                }

                if (!character.Shield.IsActive && !character.Invulnerable)
                {
                    health.TakeDamage(Damage);
                    CommonData.SetHP(-Damage);

                    character.CheckIfNotDead();
                    character.StartInvulnerability();
                }
                
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == EnemyTag) Destroy(gameObject);
        else
        {
            foreach (var tag in DestroyingTags)
            {
                if (collision.gameObject.tag == tag) Destroy(gameObject);
            }

            CheckIfCharacterCollided(collision.gameObject);
        }
    }
}
