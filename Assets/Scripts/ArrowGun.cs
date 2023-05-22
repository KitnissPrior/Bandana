using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGun : MonoBehaviour
{
    public float Offset;
    public Arrow Arrow;
    public float StartTimeBtwShots;
    public Transform[] ShotPoints;
    public bool IsActive => _isActive;
    public bool EnteredArrowArea = false;

    [SerializeField] private DamageReceiver _parent;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeBtwShots;

    private Vector2 _direction = Vector2.down;
    private bool _isActive = true;

    public void Update()
    {
        if (_isActive) Fire();
    }

    public void StartFire()
    {
        _isActive = true;
    }

    public void StopFire()
    {
        _isActive = false;
    }

    private void Fire()
    {
        if (_timeBtwShots <= 0)
        {
            foreach(var point in ShotPoints)
            {
                Bullet bullet = Instantiate(Arrow, point.position, transform.rotation);
                bullet.Initialize(_parent, _direction, _lifetime, _speed, _damage);
            }
            _timeBtwShots = StartTimeBtwShots;
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
        }
    }
}
