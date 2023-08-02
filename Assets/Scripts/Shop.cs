using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public TMP_Text MoneyText;
    public CommonData CommonData;
    public ShopItems ShopItems;

    [SerializeField] private GameObject _standardSkinButton;
    [SerializeField] private GameObject _blueSkinButton;
    [SerializeField] private GameObject _girlSkinButton;
    [SerializeField] private GameObject _stoneButton;
    [SerializeField] private GameObject _dartButton;
    [SerializeField] private GameObject _coconutButton;
    [SerializeField] private GameObject _bombButton;

    [SerializeField] private TMP_Text _standardText;
    [SerializeField] private TMP_Text _blueText;
    [SerializeField] private TMP_Text _girlText;
    [SerializeField] private TMP_Text _stoneText;
    [SerializeField] private TMP_Text _dartText;
    [SerializeField] private TMP_Text _coconutText;
    [SerializeField] private TMP_Text _bombText;

    [SerializeField] private Sprite _activeButtonSprite;
    [SerializeField] private Sprite _inactiveButtonSprite;

    private GameObject[] _skinsWithoutStandard;
    private GameObject[] _skinsWithoutBlue;
    private GameObject[] _skinsWithoutGirl;
    private string _useText = "Использовать";
    private Color _useColor = new Color(0f, 0f, 0f);
    private Color _usingColor = new Color(255f, 0f, 0f);

    void ShowMoneyInfo()
    {
        MoneyText.text = $"{CommonData.MoneyCount}";
    }

    void SetSkinButtonActive(GameObject activeButton, TMP_Text activeText, GameObject[] inactiveButtons)
    {
        activeButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _standardText.text = _useText;
        activeText.text = "Используется";
        activeText.color = _usingColor;
        foreach (var button in inactiveButtons)
        {
            button.GetComponent<Image>().sprite = _inactiveButtonSprite;
        }
    }

    void SetButtonsBackground()
    {
        if (ShopItems.StandardSkin.IsActive)
        {
            SetSkinButtonActive(_standardSkinButton, _standardText, _skinsWithoutStandard);
            if (ShopItems.BlueSkin.IsInStock)
            {
                _blueText.text = _useText;
                _blueText.color = _useColor;
            }
            if (ShopItems.GirlSkin.IsInStock)
            {
                _girlText.text = _useText;
                _girlText.color = _useColor;
            }
        }        
        else if (ShopItems.BlueSkin.IsActive)
        {
            SetSkinButtonActive(_blueSkinButton, _blueText, _skinsWithoutBlue);
            if (ShopItems.GirlSkin.IsInStock)
            {
                _girlText.text = _useText;
                _girlText.color = _useColor;
            }
        }
        else
        {
            SetSkinButtonActive(_girlSkinButton, _girlText, _skinsWithoutGirl);
            if (ShopItems.BlueSkin.IsInStock)
            {
                _blueText.text = _useText;
                _blueText.color = _useColor;
            }
        }
    }

    void Start()
    {
        _skinsWithoutStandard = new GameObject[] { _blueSkinButton, _girlSkinButton };
        _skinsWithoutBlue = new GameObject[] { _standardSkinButton, _girlSkinButton };
        _skinsWithoutGirl = new GameObject[] { _standardSkinButton, _blueSkinButton };

        ShowMoneyInfo();
        SetButtonsBackground();
    }

    void SetSkinActive(CharacterData activeSkin)
    {
        if (!activeSkin.IsActive)
        {
            CommonData.CharacterData = activeSkin;
            foreach (var skin in ShopItems.Skins)
            {
                Debug.Log(skin);
                if (skin.Name != activeSkin.Name)
                    skin.IsActive = false;
                else
                    skin.IsActive = true;
            }
            SetButtonsBackground();
        }
    }

    void SetWeaponActive(CharacterBullet activeWeapon)
    {
        CommonData.CharacterData.Gun.Bullet = activeWeapon;
        foreach (var weapon in ShopItems.Weapons)
        {
            if (weapon.Name != activeWeapon.Name)
                weapon.IsActive = false;
            else
                weapon.IsActive = true;
        }
        SetButtonsBackground();
    }

    public void SetSkinStandard()
    {
        SetSkinActive(ShopItems.StandardSkin);
    }

    void SetSkin(CharacterData skin, TMP_Text buttonText)
    {
        string[] parts = buttonText.GetParsedText().Split(' ');
        if (!skin.IsInStock && parts.Length == 3 &&
            (int.TryParse(parts[2], out int num) && CommonData.MoneyCount >= num))
        {
            skin.IsInStock = true;
            CommonData.ReduseMoney(num);
            ShowMoneyInfo();
        }
        if (skin.IsInStock)
        {
            SetSkinActive(skin);
        }
    }

    void SetWeapon(CharacterBullet weapon, TMP_Text buttonText)
    {
        string[] parts = buttonText.GetParsedText().Split(' ');
        if (!weapon.IsInStock && parts.Length == 3 &&
            (int.TryParse(parts[2], out int num) && CommonData.MoneyCount >= num))
        {
            weapon.IsInStock = true;
            CommonData.ReduseMoney(num);
            ShowMoneyInfo();
        }
        if (weapon.IsInStock)
        {
            SetWeaponActive(weapon);
        }
    }

    public void SetSkinBlue()
    {
        SetSkin(ShopItems.BlueSkin, _blueText);
    }

    public void SetSkinGirl()
    {
        SetSkin(ShopItems.GirlSkin, _girlText);
    }

    public void SetStone()
    {
        SetWeapon(ShopItems.Stone, _stoneText);
    }

    public void SetDart()
    {
        SetWeapon(ShopItems.Dart, _dartText);
    }

    public void SetCoconut()
    {
        SetWeapon(ShopItems.Coconut, _coconutText);
    }

    public void SetBomb()
    {
        SetWeapon(ShopItems.Bomb, _bombText);
    }
}
