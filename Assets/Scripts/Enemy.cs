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

    private bool _facingRight = false;
    private Rigidbody2D _rb;

    private void Start()
    {
        Health.Initialize(CharacterData.HP);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        if (Target != null && Vector3.Distance(Target.position, transform.position) < 300)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            CheckDirection();
        }
        else if (Target != null)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {

        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CheckDirection()
    {
        if (_facingRight == false && _rb.velocity.x <= 0) Flip();

        else if (_facingRight == true && _rb.velocity.x > 0) Flip();

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
    }

}
