using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health Health;
    public float Speed;
    public CharacterData CharacterData;
    public Transform Target;
    public EnemyHealthBar HealthBar;
    public int MaxDistanceToTarget = 25;

    public List<string> NoCollisionTags;
    protected bool _facingRight = false;
    protected Rigidbody2D _rb;

    public void Start()
    {
        Health.Initialize(CharacterData.HP);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        Speed = CharacterData.Speed;

        NoCollisionTags = new List<string> { 
            "InvisibleDetector",
            "Cheese",
            "Money",
            "Chest",
            "Scissors",
            "Shield"
        };
    }

    public void Move()
    {
        if (Target != null && Vector3.Distance(Target.position, transform.position) < MaxDistanceToTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            CheckDirection();
        }
    }

    public void FixedUpdate()
    {
        Move();

        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void CheckDirection()
    {
        if (!_facingRight && Target.transform.position.x > transform.position.x) 
            Flip();
        else if(_facingRight && Target.transform.position.x < transform.position.x)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block")
        {
            if (((this.transform.position.x - collision.collider.transform.position.x) < 0) ||
                (this.transform.position.x - collision.collider.transform.position.x) > 0)
            {
                Flip();
                Speed = -Speed;
                transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }
        }
        else 
            foreach(var tag in NoCollisionTags)
            {
                if (collision.gameObject.tag == tag)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
                }
            }
    }

}
