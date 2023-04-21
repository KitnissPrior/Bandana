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
    private string _trapTag = "Trap";
    private string _clewTag = "Clew";


    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _blockTag || collision.gameObject.tag == _enemyTag
            || collision.gameObject.tag == _trapTag || collision.gameObject.tag == _clewTag)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.gameObject.tag == _enemyTag)
        {
            if (health != _parent)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                enemy.HealthBar.ReduceValue(1f / health.MaxHP);
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
