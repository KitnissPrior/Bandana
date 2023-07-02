using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBullet : MonoBehaviour
{
    public int Damage;
    public LayerMask whatIsSolid;
    public List<string> DestroyingTags;
    public List<string> NoCollisionTags;
    public string EnemyTag = "Enemy";
    public DamageReceiver Parent;
    public UnityEvent OnDestroy;

    public float Speed;
    public float Lifetime;
    public float Distance;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    private float _offset = -90;

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
            "Clew",
            "Trap",
            "InvisibleDetector",
        };

        NoCollisionTags = new List<string>
        {
            "Clew",
            "Trap",
            "InvisibleDetector",
        };

        _rb = GetComponent<Rigidbody2D>();

        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + _offset);

        _rb.velocity = transform.up * Speed;

        StartCoroutine(DestroyOnLifetimeOut());
    }

    private void IgnoreSomeCollisions(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Character>(out Character character))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), character.GetComponent<Collider2D>());
        }

        foreach (var tag in NoCollisionTags)
        {
            if (collidedObject.tag == tag)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collidedObject.GetComponent<Collider2D>());
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //IgnoreSomeCollisions(collision.gameObject);

        foreach (var tag in DestroyingTags)
        {
            if (collision.gameObject.tag == tag) Destroy(gameObject);
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
        yield return new WaitForSeconds(Lifetime);
        DestroyBullet();
    }
}
