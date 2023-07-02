using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public float Offset;
    public CharacterBullet Bullet;
    public Transform ShotPoint;
    private float TimeBtwShots;
    public float StartTimeBtwShots;
    public PlayerController PlayerController;

    [SerializeField] private DamageReceiver _parent;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _lifetime;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    public void Initialize(PlayerController controller)
    {
        PlayerController = controller;
    }

    private void Fire()
    {
        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);

        if (TimeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                //CharacterBullet bullet = Instantiate(Bullet, ShotPoint.position, transform.rotation);
                Bullet.Initialize(_parent, _direction, _lifetime, _speed, _damage);
                TimeBtwShots = StartTimeBtwShots;
            }
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }*/
        if (TimeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(Bullet, ShotPoint.position, Quaternion.identity);
            }
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }
    }

    void Update()
    {
        if(!PlayerController.IsFrozen && !EventSystem.current.IsPointerOverGameObject())
        {
            Fire();
        }
    }
}
