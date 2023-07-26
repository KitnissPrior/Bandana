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

    [SerializeField] private GameObject _standardSkinButton;
    [SerializeField] private GameObject _blueSkinButton;
    [SerializeField] private GameObject _girlSkinButton;

    [SerializeField] private TMP_Text _standardText;
    [SerializeField] private TMP_Text _blueText;
    [SerializeField] private TMP_Text _girlText;

    [SerializeField] private Sprite _activeButtonSprite;
    [SerializeField] private Sprite _inactiveButtonSprite;

    private GameObject[] _skinsWithoutStandard;
    private GameObject[] _skinsWithoutBlue;
    private GameObject[] _skinsWithoutGirl;
    private string _useText = "Использовать";

    void ShowMoneyInfo()
    {
        MoneyText.text = $"{CommonData.MoneyCount}";
    }

    void SetSkinButtonActive(GameObject activeButton, TMP_Text activeText, GameObject[] inactiveButtons)
    {
        activeButton.GetComponent<Image>().sprite = _activeButtonSprite;
        _standardText.text = _useText;
        activeText.text = "Используется";
        foreach (var button in inactiveButtons)
            button.GetComponent<Image>().sprite = _inactiveButtonSprite;
    }

    void SetButtonsBackground()
    {
        if (CommonData.StandardSkin.IsActive)
        {
            SetSkinButtonActive(_standardSkinButton, _standardText, _skinsWithoutStandard);
            if (CommonData.BlueSkin.IsInStock) _blueText.text = _useText;
            if (CommonData.GirlSkin.IsInStock) _girlText.text = _useText;
        }        
        else if (CommonData.BlueSkin.IsActive)
        {
            SetSkinButtonActive(_blueSkinButton, _blueText, _skinsWithoutBlue);
            if (CommonData.GirlSkin.IsInStock) _girlText.text = _useText;
        }
        else
        {
            SetSkinButtonActive(_girlSkinButton, _girlText, _skinsWithoutGirl);
            if (CommonData.BlueSkin.IsInStock) _blueText.text = _useText;
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

    public void SetSkinStandard()
    {
        CommonData.CharacterData = CommonData.StandardSkin;
        CommonData.StandardSkin.IsActive = true;
        CommonData.BlueSkin.IsActive = false;
        CommonData.GirlSkin.IsActive = false;
        SetButtonsBackground();
    }

    public void SetSkinBlue()
    {
        if (!CommonData.BlueSkin.IsInStock && 
            (int.TryParse(_blueText.GetParsedText(), out int num) && CommonData.MoneyCount >= num))
        {
            CommonData.BlueSkin.IsInStock = true;
            CommonData.ReduseMoney(num);
            ShowMoneyInfo();
        }
        if (CommonData.BlueSkin.IsInStock)
        {
            CommonData.CharacterData = CommonData.BlueSkin;
            CommonData.BlueSkin.IsActive = true;
            CommonData.StandardSkin.IsActive = false;
            CommonData.GirlSkin.IsActive = false;
            SetButtonsBackground();
        }
    }

    public void SetSkinGirl()
    {
        if (!CommonData.GirlSkin.IsInStock && 
            (int.TryParse(_girlText.GetParsedText(), out int num) && CommonData.MoneyCount >= num))
        {
            CommonData.GirlSkin.IsInStock = true;
            CommonData.ReduseMoney(num);
            ShowMoneyInfo();
        }
        if (CommonData.GirlSkin.IsInStock)
        {
            CommonData.CharacterData = CommonData.GirlSkin;
            CommonData.GirlSkin.IsActive = true;
            CommonData.BlueSkin.IsActive = false;
            CommonData.StandardSkin.IsActive = false;
            SetButtonsBackground();
        }
    }


}
