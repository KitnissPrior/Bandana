using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int HitPoints => _hitPoints;
    public int MaxHP;
    public event Action <int> OnHit;
    public event Action<int> OnHeal;

    [SerializeField] private int _hitPoints;

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;
        OnHit?.Invoke(damage);
    }

    public void Initialize(int hP)
    {
        _hitPoints = hP;
    }

    public void Heal(int hp)
    {
        _hitPoints += hp;
        OnHeal?.Invoke(hp);
    }

}
