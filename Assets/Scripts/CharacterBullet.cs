using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBullet : MonoBehaviour
{
    public int Damage;
    public float Speed;
    public float Lifetime;
    public bool IsActive;
    public bool IsInStock;
    public bool IsAvailable;
    public string Name;

    public List<string> DestroyingTags;
    public List<string> NoCollisionTags;
    public DamageReceiver Parent;
    public UnityEvent OnDestroy;
    public float Offset = -90;

    protected Rigidbody2D _rb;
    protected Collider2D _thisCollider;
    private string _enemyTag = "Enemy";

    public void Start()
    {
        NoCollisionTags = new List<string> {
            "Clew",
            "Money",
            "Trap",
            "InvisibleDetector",
            "Character",
            "Key",
            "Chest",
            "Shield",
            "ArrowButton",
            "Scissors",
            "Cheese",
            "Crystal"
        };

        DestroyingTags = new List<string> {
            "Block",
            "Arrow",
        };

        _thisCollider = GetComponent<Collider2D>();
        IgnoreSomeCollisions();

        StartMoving();
        StartCoroutine(DestroyOnLifetimeOut());
    }

    void StartMoving()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + Offset);

        _rb.velocity = transform.up * Speed;
    }

    public void IgnoreSomeCollisions()
    {
        foreach (var tag in NoCollisionTags)
        {
            GameObject[] otherObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (var obj in otherObjects)
            {
                if(obj.TryGetComponent<Collider2D>(out Collider2D coll))
                {
                    Physics2D.IgnoreCollision(_thisCollider, coll);
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in DestroyingTags)
        {
            if (collision.gameObject.tag == tag) Destroy(gameObject);
        }

        if (collision.gameObject.TryGetComponent<Health>(out Health health) && collision.gameObject.tag == _enemyTag)
        {
            if (health != Parent)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                if (Name == "Dart") enemy.ReduceSpeed();
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

    public IEnumerator DestroyOnLifetimeOut()
    {
        yield return new WaitForSeconds(Lifetime);
        DestroyBullet();
    }
}
