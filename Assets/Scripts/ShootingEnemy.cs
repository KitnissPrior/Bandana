using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public Transform ShotPoint;
    public EnemyBullet Bullet;

    public float TimeBtwShots;
    public float StartTimeBtwShots;

    private int _shootingDistance = 5;

    public void FixedUpdate()
    {
        Move();
        Fire();

        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        if (Target != null && Vector3.Distance(Target.position, transform.position) < _maxDistanceToTarget)
        {
            Fire();
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            CheckDirection();
        }
    }

    void Fire()
    {
        if (TimeBtwShots <= 0 && Vector3.Distance(Target.position, transform.position) < _shootingDistance)
        {
            Bullet.Initialize(Target);
            Vector3 bulletPosition = GetBulletPosition();
            EnemyBullet bullet = Instantiate(Bullet, bulletPosition, Target.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddRelativeForce(Vector3.forward * 3, ForceMode2D.Impulse);

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
