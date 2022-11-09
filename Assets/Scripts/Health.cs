using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int HitPoints { get; private set; }
    public event Action <int> OnHit;
    public event Action<int> OnHeal;

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        OnHit?.Invoke(damage);
    }

    public void Heal(int hp)
    {
        HitPoints += hp;
        OnHeal?.Invoke(hp);
    }

}
