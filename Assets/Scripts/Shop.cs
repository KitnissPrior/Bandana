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
    public ShopProperties ShopProperties;

    [SerializeField] private Sprite _activeButtonSprite;
    [SerializeField] private Sprite _inactiveButtonSprite;

    private Color _useColor = new Color(0f, 0f, 0f);
    private Color _usingColor = new Color(193f, 193f, 193f);

    void ShowMoneyInfo()
    {
        MoneyText.text = $"{CommonData.MoneyCount}";
    }

    void SetButtonsBackground(List<ShopItem> items)
    {
        foreach(var item in items)
        {
            if (item.IsActive)
            {
                item.ChangeButtonView(_activeButtonSprite, _usingColor);
            }
            else if (item.IsInStock)
                item.ChangeButtonView(_inactiveButtonSprite, _useColor);
        }
    }

    void Start()
    {
        ShowMoneyInfo();
        ShopProperties.SetProperties();
        ShopItems.SetItems();
        if(ShopItems.Skins[0] != null)
            SetButtonsBackground(ShopItems.Skins);
        if (ShopItems.Weapons[0] != null)
            SetButtonsBackground(ShopItems.Weapons);
    }

    void SetItemActive(ShopItem selectedItem, List<ShopItem> items)
    {
        if (!selectedItem.IsActive)
        {
            foreach (var item in items)
            {
                if (item.Name != selectedItem.Name)
                {
                    item.IsActive = false;
                }
                else
                {
                    item.IsActive = true;
                }
            }
            SetButtonsBackground(items);
        }
    }

    void UseSkin(CharacterData skin)
    {
        CommonData.CharacterData = skin;
    }

    void UseWeapon(CharacterBullet weapon)
    {
        CommonData.CharacterData.Gun.Bullet = weapon;
    }

    void ChooseItem(ShopItem item)
    {
        string[] parts = item.ButtonText.GetParsedText().Split(' ');
        if (!item.IsInStock && parts.Length == 3 &&
           (int.TryParse(parts[2], out int num) && CommonData.MoneyCount >= num))
        {
            item.IsInStock = true;
            CommonData.ReduseMoney(num);
            ShowMoneyInfo();
        }
    }

    void ChooseSkin(CharacterData skin)
    {
        ShopItem item = skin.ShopItem;
        ChooseItem(item);
        if (item.IsInStock)
        {
            UseSkin(skin);
            SetItemActive(item, ShopItems.Skins);
        }
    }

    void ChooseWeapon(CharacterBullet weapon)
    {
        ShopItem item = weapon.ShopItem;
        ChooseItem(item);
        if (item.IsInStock)
        {
            UseWeapon(weapon);
            SetItemActive(item, ShopItems.Weapons);
        }
    }

    public void ChooseSkinStandard()
    {
        UseSkin(ShopItems.StandardSkin);
        SetItemActive(ShopItems.StandardSkin.ShopItem, ShopItems.Skins);
    }

    public void ChooseBlueSkin()
    {
        ChooseSkin(ShopItems.BlueSkin);
    }

    public void ChooseGirlSkin()
    {
        ChooseSkin(ShopItems.GirlSkin);
    }

    public void ChooseStone()
    {
        UseWeapon(ShopItems.Stone);
        SetItemActive(ShopItems.Stone.ShopItem, ShopItems.Weapons);
    }

    public void ChooseDart()
    {
        ChooseWeapon(ShopItems.Dart);
    }

    public void ChooseCoconut()
    {
        ChooseWeapon(ShopItems.Coconut);
    }

    public void ChooseBomb()
    {
        ChooseWeapon(ShopItems.Bomb);
    }
}
