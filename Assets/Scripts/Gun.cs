using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset;
    public Bullet Bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;

    [SerializeField] private DamageReceiver _parent;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Bullet bullet = Instantiate(Bullet, shotPoint.position, transform.rotation);
                bullet.Initialize(_parent, _direction, _lifetime, _speed, _damage);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
