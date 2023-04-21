using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _cheeseHP = 1;
    [SerializeField] private TMP_Text _cheeseText;
    [SerializeField] private TMP_Text _healthComment;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _eatButton;
    [SerializeField] private TMP_Text _scissorsText;
    [SerializeField] private GameObject _useScissorsButton;

    private Character _character;
    private int _cheeseCount;
    private int _scissorsCount;
    private bool _isFrozen;

    public void Initialize(Character player)
    {
        _character = player;
        _cheeseCount = 0;
        _scissorsCount = 0;
    }

    void ShowCheeseInfo ()
    {
        _cheeseText.text = $"Сыра: {_cheeseCount}";

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

    void showScissorsInfo()
    {
        _scissorsText.text = $"Ножниц: {_scissorsCount}";

        if(_scissorsCount > 0 && _isFrozen)
        {
            _useScissorsButton.SetActive(true);
        }
        else
        {
            _useScissorsButton.SetActive(false);
        }
    }

    public void Update()
    {
        ShowCheeseInfo();
        showScissorsInfo();
        if(_character)
        {
            _isFrozen = _character.GetComponent<PlayerController>().IsFrozen;
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

}
