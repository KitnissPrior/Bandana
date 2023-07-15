using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public Transform ShotPoint;
    public EnemyBullet Bullet;

    public float TimeBtwShots;
    public float StartTimeBtwShots;

    protected int _shootingDistance = 6;
    protected float _offset = 0f;

    public void FixedUpdate()
    {
        Move();
        Fire();
        TakeAim();

        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeAim()
    {
        Vector3 difference = Target.transform.position - transform.position;
        float rot2 = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        ShotPoint.rotation = Quaternion.Euler(0f, 0f, rot2 + _offset);
    }

    public void Move()
    {
        if (Target != null && Vector3.Distance(Target.position, transform.position) < MaxDistanceToTarget)
        {
            Fire();
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            CheckDirection();
        }
    }

    public void CheckDirection()
    {
        if (!_facingRight && Target.transform.position.x > transform.position.x)
        {
            Flip();
            _offset = 0f;
            Bullet.Direction = Vector2.right;
        }
        else if (_facingRight && Target.transform.position.x < transform.position.x)
        {
            Flip();
            _offset = 180f;
            Bullet.Direction = Vector2.left;
        }
    }

    void Fire()
    {
        if (TimeBtwShots <= 0 && Vector3.Distance(Target.position, transform.position) < _shootingDistance)
        {
            Instantiate(Bullet, ShotPoint.position, ShotPoint.rotation);

            TimeBtwShots = StartTimeBtwShots;
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }
    }

    Vector3 GetBulletPosition()
    {
        if (Vector3.Distance(Target.position, transform.position) < 0f)
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
            return ShotPoint.position;
        }
        return ShotPoint.position;
    }

}
