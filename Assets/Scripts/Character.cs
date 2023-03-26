using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public PlayerRun PlayerRun;
    public Health Health;
    public Gun Gun;
    public Transform GunPosition;
    public CharacterData CharacterData;
    public HealthView HealthView;
    public Inventory Inventory;
    public float FreezingDelay = 3f;

    [SerializeField] private string _badEndScene;
    [SerializeField] private string _goodEndScene;

    internal void Initialize(CharacterData characterData, HealthView healthView, Inventory inventory)
    {
        CharacterData = characterData;
        HealthView = healthView;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);

        Gun = Instantiate(CharacterData.Gun, GunPosition);
        Gun.Initialize(gameObject.GetComponent<PlayerController>());

        Inventory = inventory;
        Inventory.Initialize(this);
    }

    private void CheckIfNotDead()
    {
        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(_badEndScene);
        }
    }

    private void CheckIfEnemyCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Health.TakeDamage(enemy.CharacterData.Damage);
            HealthView.HP -= enemy.CharacterData.Damage;

            CheckIfNotDead();
        }
    }

    public void UnfreezeCharacter()
    {
        gameObject.GetComponent<PlayerController>().IsFrozen = false;
    }

    private void CheckIfClewCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Clew")
        {
            Destroy(collidedObject);
            gameObject.GetComponent<PlayerController>().IsFrozen = true;
            Invoke("UnfreezeCharacter", FreezingDelay);
        }
    }

    private void CheckIfCheeseCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Cheese")
        {
            Inventory.AddCheese();
            Destroy(collidedObject);
        }
    }

    private void CheckIfScissorsCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Scissors")
        {
            Inventory.AddScissors();
            Destroy(collidedObject);
        }
    }

    private void CheckIfTrapCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Trap>(out Trap trap))
        {
            Health.TakeDamage(trap.Damage);
            HealthView.HP -= trap.Damage;

            CheckIfNotDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfEnemyCollided(collision.gameObject);
        CheckIfCheeseCollided(collision.gameObject);
        CheckIfClewCollided(collision.gameObject);
        CheckIfScissorsCollided(collision.gameObject);
        CheckIfTrapCollided(collision.gameObject);
    }

}
