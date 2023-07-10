using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : CharacterBullet
{
    public Transform Target;

    [SerializeField] private float _distance;
    [HideInInspector] public Vector2 Direction;

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
            "ArrowButton"
        };

        DestroyingTags = new List<string> {
            "Block",
            "Arrow",
        };

        _thisCollider = GetComponent<Collider2D>();
        IgnoreSomeCollisions();

        Destroy(gameObject, Lifetime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in DestroyingTags)
        {
            if (collision.gameObject.tag == tag) Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}