using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : CharacterBullet
{
    public Transform Target;

    public void Initialize(Transform target)
    {
        Target = target;
    }

    void Start()
    {
        NoCollisionTags = new List<string> {
            "Clew",
            "Trap",
            "InvisibleDetector",
            "Key",
            "Enemy",
            "Scissors",
            "Shield",
            "Cheese",
            "Chest",
        };

        DestroyingTags = new List<string> {
            "Block",
            "Arrow",
            "ArrowButton"
        };

        Offset = 0;
        _thisCollider = GetComponent<Collider2D>();
        IgnoreSomeCollisions();

        StartMoving();
        StartCoroutine(DestroyOnLifetimeOut());
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in DestroyingTags)
        {
            if (collision.gameObject.tag == tag) Destroy(gameObject);
        }
    }

    void StartMoving()
    {
        _rb = GetComponent<Rigidbody2D>();

        Vector3 diference = Camera.main.ScreenToWorldPoint(Target.position) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + Offset);

        _rb.velocity = transform.up * Speed;
    }
}
