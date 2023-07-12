using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBoss : ShootingEnemy
{
    public EnemyBullet Liana;
    public GameObject Key;
    public Transform KeyPoint;

    public bool IsCharacterEntered = false;
    public int FireDelay = 3;

    private int _bulletsCount = 0;

    void Start()
    {
        base.Start();
        _bulletsCount = 0;
    }

    public void FixedUpdate()
    {
        if (IsCharacterEntered)
        {
            Move();
            TakeAim();
            Invoke("Fire", FireDelay);
        }

        if (Health.HitPoints <= 0)
        {
            GameObject key = Instantiate(Key, KeyPoint.position, KeyPoint.rotation);
            Destroy(gameObject);
        }
    }

    public void CheckDirection()
    {
        if (!_facingRight && Target.transform.position.x > transform.position.x)
        {
            Flip();
            _offset = 0f;
            Bullet.Direction = Vector2.right;
            Liana.Direction = Vector2.left;
        }
        else if (_facingRight && Target.transform.position.x < transform.position.x)
        {
            Flip();
            _offset = 180f;
            Bullet.Direction = Vector2.left;
            Liana.Direction = Vector2.right;
        }
    }

    void Fire()
    {
        if (gameObject != null && TimeBtwShots <= 0 && Vector3.Distance(Target.position, transform.position) < _shootingDistance)
        {
            _bulletsCount++;
            switch (_bulletsCount)
            {
                case 1:
                    Instantiate(Liana, ShotPoint.position, ShotPoint.rotation);
                    break;
                case 2:
                    Instantiate(Bullet, ShotPoint.position, ShotPoint.rotation);
                    break;
                case 3:
                    Instantiate(Bullet, ShotPoint.position, ShotPoint.rotation);
                    _bulletsCount = 0;
                    break;
                default:
                    _bulletsCount = 0;
                    break;
            }
            TimeBtwShots = StartTimeBtwShots;
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }
    }

}
