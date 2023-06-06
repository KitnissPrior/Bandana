using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
 
    public Collider2D Collider;
    public UnityEvent OnDestroy; 
    public float Distance;
    public int Damage;
    public DamageReceiver Parent;
    public List<string> DestroyingTags;
    public List<string> NoCollisionTags;
    public string EnemyTag = "Enemy";

    private Vector2 _direction;
    private float _lifetime;
    private float _speed;

    void Start()
    {
        DestroyingTags = new List<string> {
            "Block",
            "Arrow",
            "ArrowButton",
            "Scissors",
            "Shield",
            "Cheese",
            "Key",
            "Chest",
        };

        NoCollisionTags = new List<string>
        {
            "Clew",
            "Trap",
            "InvisibleDetector",
            "Character",
        };
    }

    public void Initialize(DamageReceiver parent, Vector2 direction, float lifetime, float speed, int damage)
    {
        Parent = parent;
        _direction = direction;
        _lifetime = lifetime;
        _speed = speed;
        Damage = damage;

        StartCoroutine(DestroyOnLifetimeOut());
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime);
    }

    private void IgnoreSomeCollisions(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Character>(out Character character))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), character.GetComponent<Collider2D>());
        }

        foreach(var tag in NoCollisionTags)
        {
            if (collidedObject.tag == tag)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collidedObject.GetComponent<Collider2D>());
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        IgnoreSomeCollisions(collision.gameObject);

        foreach (var tag in DestroyingTags)
        {
            if(collision.gameObject.tag == tag) Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.gameObject.tag == EnemyTag)
        {
            if (health != Parent)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                enemy.HealthBar.ReduceValue(1f / health.MaxHP);
                health.TakeDamage(Damage);

                Destroy(gameObject);
            }             
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
        OnDestroy.Invoke();
    }

    private IEnumerator DestroyOnLifetimeOut()
    {
        yield return new WaitForSeconds(_lifetime);
        DestroyBullet();
    }
}
