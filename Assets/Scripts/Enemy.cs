using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public Health Health;
  public float Speed;
  public CharacterData CharacterData;

    [SerializeField] private EnemiesCountManager _enemiesCountManager;

    private void Start()
    {
        Health.Initialize(CharacterData.HP);
    }
    private void Update()
  {
        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
            _enemiesCountManager.EnemiesCount--;
        }
     //transform.Translate(Vector2.right * Speed * Time.deltaTime);
  }
}
