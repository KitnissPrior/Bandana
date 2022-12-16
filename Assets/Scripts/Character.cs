using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public PlayerRun PlayerRun;
    public Health Health;
    public Gun Gun;
    public Transform GunPosition;
    public CharacterData CharacterData;
    public HealthView HealthView;

    [SerializeField] private string _badEndScene;
    [SerializeField] private string _goodEndScene;

    internal void Initialize(CharacterData characterData, HealthView healthView)
    {
        CharacterData = characterData;
        HealthView = healthView;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);
        Gun = Instantiate(CharacterData.Gun, GunPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Health.TakeDamage(enemy.CharacterData.Damage);
            HealthView.HP -= enemy.CharacterData.Damage;
 
            if (Health.HitPoints <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(_badEndScene);
            }
        }
    }

}
