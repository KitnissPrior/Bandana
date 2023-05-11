using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoint : MonoBehaviour
{
    public float Offset;
    public Arrow Arrow;
    private float TimeBtwShots;
    public float StartTimeBtwShots;

    [SerializeField] private DamageReceiver _parent;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;


    private void Fire()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Offset);

        if (TimeBtwShots <= 0)
        {
            Bullet bullet = Instantiate(Arrow, transform.position, transform.rotation);
            bullet.Initialize(_parent, Vector2.down, _lifetime, _speed, _damage);
            TimeBtwShots = StartTimeBtwShots;
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }
    }
}
