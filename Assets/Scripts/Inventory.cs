using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Message ArrowMessage;
    public Message ClewMessage;
    public Message ScissorsMessage;
    public Message CheeseMessage;
    public Message ShieldMessage;
    public Message FightMessage;

    public CommonData CommonData;

    public int MaxCrystalsCount;

    [SerializeField] private int _cheeseHP = 1;

    [SerializeField] private TMP_Text _cheeseText;
    [SerializeField] private TMP_Text _shieldText;
    [SerializeField] private TMP_Text _scissorsText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _crystalsText;

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
    private int _crystalsCount;
    private int _itemWeight = 1;

    private Shield _shieldCircle;
    private ProgressBar _shieldBar;
    private Message[] _messages;

    public void Initialize(Character player, Shield shieldCircle, ProgressBar shieldBar)
    {
        _character = player;
        _shieldCircle = shieldCircle;
        _shieldBar = shieldBar;

        _cheeseCount = CommonData.CheeseCount;
        _scissorsCount = CommonData.ScissorsCount;
        _shieldsCount = CommonData.ShieldsCount;
        _crystalsCount = 0;

        ShowMoneyInfo();

        _messages = new Message[]{
            ArrowMessage,
            CheeseMessage,
            ScissorsMessage,
            ClewMessage,
            FightMessage,
        };
    }

    string GetItemInfo(int count)
    {
        if (count < 10) return $" {count}";

        return $"{count}";
    }

    void ShowCheeseInfo ()
    {
        _cheeseText.text = GetItemInfo(CommonData.CheeseCount);

        if (CommonData.CheeseCount > 0)
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
        _scissorsText.text = GetItemInfo(CommonData.ScissorsCount);

        if(CommonData.ScissorsCount > 0)
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
        _shieldText.text = GetItemInfo(CommonData.ShieldsCount);

        if (CommonData.ShieldsCount > 0)
        {
            _useShieldButton.GetComponent<Image>().sprite = _brightShield;
        }
        else
        {
            _useShieldButton.GetComponent<Image>().sprite = _transparentShield;
        }
    }

    void ShowMoneyInfo()
    {
        _moneyText.text = $"{CommonData.MoneyCount}";
    }

    void ShowCrystalsInfo()
    {
        _crystalsText.text = $"{_crystalsCount}/{MaxCrystalsCount}";
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

    private bool MessageActive(Message message)
    {
        return message && message.MessageWindow.gameObject.active;
    }

    private void SetMessageInactive(Message message)
    {
        message.MessageWindow.gameObject.SetActive(false);
    }

    private void CloseUnneccessaryMessages()
    {
        for(int i = 1; i < _messages.Length; i++)
        {
            if (MessageActive(_messages[i]))
            {
                for(int j = 0; j < i; j++)
                {
                    if (MessageActive(_messages[j]))
                        SetMessageInactive(_messages[j]);
                }
            }
        }
    }

    public void Update()
    {
        ShowCheeseInfo();
        ShowScissorsInfo();
        ShowShieldInfo();

        CloseUnneccessaryMessages();

        if (_character)
        {
            _isFrozen = _character.GetComponent<PlayerController>().IsFrozen;
            _shieldCircle.Update();
        }

        ControlUsingItemsByKeyBoard();
    }

    public void AddCheese()
    {
        CommonData.SetCheeseCount(_itemWeight);
    }

    public void AddMoney(int count)
    {
        CommonData.AddMoney(count);
        ShowMoneyInfo();
    }

    public void AddCrystal()
    {
        _crystalsCount++;
        ShowCrystalsInfo();
    }

    public void EatCheese()
    {
        if (CheeseMessage) Destroy(CheeseMessage.gameObject);

        if (CommonData.CheeseCount > 0 && _character.Health.HitPoints < _character.Health.MaxHP)
        {
            CommonData.SetCheeseCount(-_itemWeight);
            _character.Health.Heal(_cheeseHP);
        }
    }

    public void AddScissors()
    {
        CommonData.SetScissorsCount(_itemWeight);
    }

    public void UseScissors()
    {
        if (ScissorsMessage) Destroy(ScissorsMessage.gameObject);

        if (CommonData.ScissorsCount > 0 && _isFrozen)
        {
            CommonData.SetScissorsCount(-_itemWeight);
            _character.BindingBar.ReduceTimeLeft(_character.FreezingDelay);
            _character.UnfreezeCharacter();

            if (ClewMessage) Destroy(ClewMessage.gameObject);
        }
    }

    public void AddShield()
    {
        CommonData.SetShieldsCount(_itemWeight);
    }

    public void DeactivateShield()
    {
        _shieldCircle.IsActive = false;
    }

    public void UseShield()
    {
        if(CommonData.ShieldsCount > 0 && !_shieldCircle.IsActive)
        {
            CommonData.SetShieldsCount(-_itemWeight);
            _shieldCircle.IsActive = true;

            _shieldBar.Value = 1f;

            if (ShieldMessage)
            {
                Time.timeScale = 1;
                Destroy(ShieldMessage.gameObject);
            }

            StartCoroutine(_character.UpdateProgressBar(_shieldBar, _shieldCircle.ProtectingTime));
            Invoke("DeactivateShield", _shieldCircle.ProtectingTime);
        }
    }

}
