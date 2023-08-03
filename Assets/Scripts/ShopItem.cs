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

    public void ChangeButtonView(Sprite sprite, Color color, bool isCoinActive)
    {
        Button.GetComponent<Image>().sprite = sprite;
        ButtonText.color = color;
        ButtonText.text = "";
        if (CoinSprite != null) CoinSprite.SetActive(isCoinActive);
    }
}
