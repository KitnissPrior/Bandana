using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string Name;
    public bool IsActive;
    public bool IsAvailable;
    public bool IsInStock;
    public TMP_Text ButtonText;
    public GameObject Button;
    public GameObject CoinSprite;

    internal void Initialize(TMP_Text buttonText, GameObject button, GameObject coinSprite)
    {
        if(Button == null)
        {
            ButtonText = buttonText;
            Button = button;
            CoinSprite = coinSprite;
        }
    }

    public void ChangeButtonView(Sprite sprite, Color color)
    {
        Button.GetComponent<Image>().sprite = sprite;
        ButtonText.color = color;
        ButtonText.text = "";
        if (CoinSprite != null) CoinSprite.SetActive(false);
    }
}
