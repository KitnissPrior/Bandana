using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _cheeseHP = 1;

    [SerializeField] private TMP_Text _cheeseText;
    [SerializeField] private TMP_Text _shieldText;
    [SerializeField] private TMP_Text _scissorsText;

    [SerializeField] private Sprite _transparentScissors;
    [SerializeField] private Sprite _transparentCheese;
    [SerializeField] private Sprite _transparentShield;

    [SerializeField] private Sprite _brightCheese;
    [SerializeField] private Sprite _brightShield;
    [SerializeField] private Sprite _brightScissors;

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

    string GetItemInfo(int count)
    {
        if (count < 10) return $" {count}";

        return $"{count}";
    }

    void ShowCheeseInfo ()
    {
        _cheeseText.text = GetItemInfo(_cheeseCount);

        if (_cheeseCount > 0)
        {
            _eatButton.GetComponent<Image>().sprite = _brightCheese;
        }
        else
        {
            _eatButton.GetComponent<Image>().sprite = _transparentCheese;
        }
    }

    void ShowScissorsInfo()
    {
        _scissorsText.text = GetItemInfo(_scissorsCount);

        if(_scissorsCount > 0)
        {
            _useScissorsButton.GetComponent<Image>().sprite = _brightScissors;
        }
        else
        {
            _useScissorsButton.GetComponent<Image>().sprite = _transparentScissors;
        }
    }

    void ShowShieldInfo()
    {
        _shieldText.text = GetItemInfo(_shieldsCount);

        if (_shieldsCount > 0)
        {
            _useShieldButton.GetComponent<Image>().sprite = _brightShield;
        }
        else
        {
            _useShieldButton.GetComponent<Image>().sprite = _transparentShield;
        }
    }

    void ControlUsingItemsByKeyBoard()
    {
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1)) 
            UseScissors();
        if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2)) 
            EatCheese();
        if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3)) 
            UseShield();
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

        ControlUsingItemsByKeyBoard();
    }

    public void AddCheese()
    {
        _cheeseCount++;
    }

    public void EatCheese()
    {
        if(_cheeseCount > 0 && _character.Health.HitPoints < _character.Health.MaxHP)
        {
            _cheeseCount--;

            _character.Health.Heal(_cheeseHP);
            _character.HealthView.HP += _cheeseHP;
        }
    }

    public void AddScissors()
    {
        _scissorsCount++;
    }

    public void UseScissors()
    {
        if(_scissorsCount > 0 && _isFrozen)
        {
            _scissorsCount--;
            _character.BindingBar.ReduceTimeLeft(_character.FreezingDelay);
            _character.UnfreezeCharacter();
        }
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
        if(_shieldsCount > 0)
        {
            _shieldsCount--;
            _shieldCircle.IsActive = true;

            _shieldBar.Value = 1f;

            StartCoroutine(_character.UpdateProgressBar(_shieldBar, _shieldCircle.ProtectingTime));
            Invoke("DeactivateShield", _shieldCircle.ProtectingTime);
        }
    }

}
