using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    private string _characterTag = "Character";

    private void CheckIfCharacterCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Health>(out Health health) && collidedObject.tag == _characterTag)
        {
            if (health != Parent)
            {
                Character character = collidedObject.GetComponent<Character>();

                if (!character.Shield.IsActive)
                {
                    health.TakeDamage(Damage);
                    character.HealthView.HP -= Damage;

                    character.CheckIfNotDead();
                }
                
                Destroy(gameObject);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in ItemTags)
        {
            if (collision.gameObject.tag == tag) Destroy(gameObject);
        }

        CheckIfCharacterCollided(collision.gameObject);
    }
}
