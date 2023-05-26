using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public ProgressBar BindingBar;
    public ProgressBar ShieldBar;
    public Shield Shield;
    public bool HasKey => _hasKey;
    /// <summary>
    /// Неуязвимый
    /// </summary>
    public bool Invulnerable => _invulnerable;

    [SerializeField] private string _badEndScene;
    [SerializeField] private string _goodEndScene;

    private int _barSmoothingCoeff = 10;
    private bool _hasKey = false;

    private Collider2D _characterCollider;
    private Collider2D _shieldCollider;
    private float _restartCollisionDelay = 0.8f;

    private float _invulnerabilityDelay = 2f;
    private bool _invulnerable = false;

    internal void Initialize(CharacterData characterData, HealthView healthView, Inventory inventory,
        ProgressBar bindingBar, ProgressBar shieldBar)
    {
        CharacterData = characterData;
        HealthView = healthView;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);

        BindingBar = bindingBar;
        ShieldBar = shieldBar;

        Gun = Instantiate(CharacterData.Gun, GunPosition);
        Gun.Initialize(gameObject.GetComponent<PlayerController>());

        Inventory = inventory;
        Inventory.Initialize(this, Shield, ShieldBar);

        _characterCollider = gameObject.GetComponent<Collider2D>();
        _shieldCollider = Shield.GetComponent<Collider2D>();
    }

    public void CheckIfNotDead()
    {
        if (Health.HitPoints <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(_badEndScene);
        }
    }

    private void CheckIfKeyCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Key")
        {
            Destroy(collidedObject);
            _hasKey = true;
        }
    }

    /// <summary>
    /// Выключает эффект неуязвимости
    /// </summary>
    /// <returns></returns>
    private IEnumerator StopInvulnerability()
    {
        yield return new WaitForSeconds(_invulnerabilityDelay);
        _invulnerable = false;
    }

    /// <summary>
    /// Включает эффект неуязвимости
    /// </summary>
    /// <returns></returns>
    public void StartInvulnerability()
    {
        _invulnerable = true;
        StartCoroutine(StopInvulnerability());
    }

    private void CheckIfEnemyCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Enemy>(out Enemy enemy) && !Shield.IsActive && !Invulnerable)
        {
            Health.TakeDamage(enemy.CharacterData.Damage);
            HealthView.HP -= enemy.CharacterData.Damage;
            CheckIfNotDead();

            StartInvulnerability();
        }
    }

    private IEnumerator RestartCollision(Collider2D collider)
    {
        yield return new WaitForSeconds(_restartCollisionDelay);
        Physics2D.IgnoreCollision(collider, _characterCollider, false);
    }

    private void CheckIfTrapCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Trap>(out Trap trap))
        {
            Collider2D trapCollider = collidedObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(trapCollider, _characterCollider, true);

            if (Shield.IsActive)
            {
                Physics2D.IgnoreCollision(trapCollider, _shieldCollider);
            }
            else
            {
                Health.TakeDamage(trap.Damage);
                HealthView.HP -= trap.Damage;
                CheckIfNotDead();
            }

            StartCoroutine(RestartCollision(trapCollider));
        }
    }

    public void UnfreezeCharacter()
    {
        gameObject.GetComponent<PlayerController>().IsFrozen = false;
    }

    public IEnumerator UpdateProgressBar(ProgressBar progressBar, int delay)
    {
        int count = _barSmoothingCoeff * delay;

        for (int i = 0; i < count; i++)
        {
            progressBar.ReduceTimeLeft(1f / count);
            yield return new WaitForSeconds(1f / _barSmoothingCoeff);
        }
    }

    private void CheckIfClewCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Clew")
        {
            Destroy(collidedObject);

            if (Shield.IsActive)
            {
                Inventory.DeactivateShield();
                ShieldBar.ReduceTimeLeft(Shield.ProtectingTime);
            }
            else
            {
                BindingBar.Value = 1f;

                gameObject.GetComponent<PlayerController>().IsFrozen = true;

                StartCoroutine(UpdateProgressBar(BindingBar, FreezingDelay));

                Invoke("UnfreezeCharacter", FreezingDelay);
            }
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

    private void CheckIfShieldCollided(GameObject collidedObject)
    {
        if (collidedObject.tag == "Shield")
        {
            Destroy(collidedObject);
            Inventory.AddShield();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfEnemyCollided(collision.gameObject);
        CheckIfTrapCollided(collision.gameObject);
        CheckIfClewCollided(collision.gameObject);

        CheckIfScissorsCollided(collision.gameObject);
        CheckIfCheeseCollided(collision.gameObject);
        CheckIfShieldCollided(collision.gameObject);

        CheckIfKeyCollided(collision.gameObject);
    }

}
