using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public float Offset;
    public CharacterBullet Bullet;
    public Transform ShotPoint;
    public float TimeBtwShots;
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
        if (TimeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(Bullet, ShotPoint.position, Quaternion.identity);
                TimeBtwShots = StartTimeBtwShots;
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
