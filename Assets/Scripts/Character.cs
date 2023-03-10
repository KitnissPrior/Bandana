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
    public Inventory Inventory;

    [SerializeField] private string _badEndScene;
    [SerializeField] private string _goodEndScene;

    internal void Initialize(CharacterData characterData, HealthView healthView, Inventory inventory)
    {
        CharacterData = characterData;
        HealthView = healthView;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);
        Gun = Instantiate(CharacterData.Gun, GunPosition);

        Inventory = inventory;
        Inventory.Initialize(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Health.TakeDamage(enemy.CharacterData.Damage);
            HealthView.HP -= enemy.CharacterData.Damage;
 
            if (Health.HitPoints <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(_badEndScene);
            }
        }

        if (collision.gameObject.tag == "Cheese") 
        {
            Inventory.AddCheese();
            
            Destroy(collision.gameObject);
        }
    }

}
