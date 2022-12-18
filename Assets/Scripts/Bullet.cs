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

    private DamageReceiver _parent;
    private Vector2 _direction;
    private float _lifetime;
    private float _speed;
    private int _damage;

    private string _blockTag = "Block";
    private string _enemyTag = "Enemy";


    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _blockTag || collision.gameObject.tag == _enemyTag)
        {
            Destroy(gameObject);
        }

        if(collision.TryGetComponent<Health>(out Health health))
        {
            if (health != _parent)
            {
                health.TakeDamage(_damage);
                Destroy(gameObject);
            }             
        }
    }

    public void Initialize(DamageReceiver parent, Vector2 direction, float lifetime, float speed, int damage)
    {
        _parent = parent;
        _direction = direction;
        _lifetime = lifetime;
        _speed = speed;
        _damage = damage;

        StartCoroutine(DestroyOnLifetimeOut());
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
