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
    public int FreezingDelay = 5;
    public BindingBar BindingBar;

    [SerializeField] private string _badEndScene;
    [SerializeField] private string _goodEndScene;

    private int _smoothingFreezeCoeff = 10;

    internal void Initialize(CharacterData characterData, HealthView healthView, Inventory inventory, BindingBar bindingBar)
    {
        CharacterData = characterData;
        HealthView = healthView;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);

        BindingBar = bindingBar;

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

    IEnumerator UpdateBindingBar()
    {
        int count = _smoothingFreezeCoeff * FreezingDelay;

        for (int i = 0; i < count; i++)
        {
            BindingBar.ReduceTimeLeft(1f / count);
            yield return new WaitForSeconds(1f / _smoothingFreezeCoeff);
        }
    }

    private void CheckIfClewCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Clew")
        {
            BindingBar.Value = 1f;

            Destroy(collidedObject);
            gameObject.GetComponent<PlayerController>().IsFrozen = true;

            StartCoroutine(UpdateBindingBar());

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
