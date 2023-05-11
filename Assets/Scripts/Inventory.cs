using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _cheeseHP = 1;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _healthComment;

    [SerializeField] private TMP_Text _cheeseText;
    [SerializeField] private TMP_Text _shieldText;
    [SerializeField] private TMP_Text _scissorsText;

    [SerializeField] private GameObject _useScissorsButton;
    [SerializeField] private GameObject _eatButton;
    [SerializeField] private GameObject _useShieldButton;

    private Character _character;
    private bool _isFrozen;

    private int _cheeseCount;
    private int _scissorsCount;
    private int _shieldsCount;

    private Shield _shieldCircle;
    private ProgressBar _shieldBar;

    public void Initialize(Character player, Shield shieldCircle, ProgressBar shieldBar)
    {
        _character = player;
        _shieldCircle = shieldCircle;
        _shieldBar = shieldBar;

        _cheeseCount = 0;
        _scissorsCount = 0;
        _shieldsCount = 0;
    }

    void ShowCheeseInfo ()
    {
        _cheeseText.text = $"{_cheeseCount} шт.";

        int HP = _character.Health.HitPoints;
        int maxHP = _character.Health.MaxHP;

        if (HP == maxHP)
        {
            _healthComment.text = "Здоровье максимальное";
            _eatButton.SetActive(false);
        }
        if (HP < maxHP && _cheeseCount == 0)
        {
            _healthComment.text = "";
            _eatButton.SetActive(false);
        }
        if (HP < maxHP && _cheeseCount > 0)
        {
            _healthComment.text = "";
            _eatButton.SetActive(true);
        }
    }

    void ShowScissorsInfo()
    {
        _scissorsText.text = $"{_scissorsCount} шт.";

        if(_scissorsCount > 0 && _isFrozen)
        {
            _useScissorsButton.SetActive(true);
        }
        else
        {
            _useScissorsButton.SetActive(false);
        }
    }

    void ShowShieldInfo()
    {
        _shieldText.text = $"{_shieldsCount} шт.";

        if (_shieldsCount > 0)
        {
            _useShieldButton.SetActive(true);
        }
        else
        {
            _useShieldButton.SetActive(false);
        }
    }

    public void Update()
    {
        ShowCheeseInfo();
        ShowScissorsInfo();
        ShowShieldInfo();

        if (_character)
        {
            _isFrozen = _character.GetComponent<PlayerController>().IsFrozen;
            _shieldCircle.Update();
        }
    }

    public void OpenInventory()
    {
        if (!_panel.activeSelf)
        {
            _panel.SetActive(true);
        }
    }

    public void CloseInventory()
    {
        _panel.SetActive(false);
    }

    public void AddCheese()
    {
        _cheeseCount++;
    }

    public void EatCheese()
    {
        _cheeseCount--;

        _character.Health.Heal(_cheeseHP);
        _character.HealthView.HP += _cheeseHP;
    }

    public void AddScissors()
    {
        _scissorsCount++;
    }

    public void UseScissors()
    {
        _scissorsCount--;
        _character.BindingBar.ReduceTimeLeft(_character.FreezingDelay);
        _character.UnfreezeCharacter();
    }

    public void AddShield()
    {
        _shieldsCount++;
    }

    public void DeactivateShield()
    {
        _shieldCircle.IsActive = false;
    }

    public void UseShield()
    {
        _shieldsCount--;
        _shieldCircle.IsActive = true;

        _shieldBar.Value = 1f;

        StartCoroutine(_character.UpdateProgressBar(_shieldBar, _shieldCircle.ProtectingTime));
        Invoke("DeactivateShield", _shieldCircle.ProtectingTime);
    }

}
