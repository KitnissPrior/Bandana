using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopProperties : MonoBehaviour
{
    public ShopItem StandardItem;
    public ShopItem BlueItem;
    public ShopItem GirlItem;
    public ShopItem StoneItem;
    public ShopItem DartItem;
    public ShopItem CoconutItem;
    public ShopItem BombItem;
    public ShopItems ShopItems;

    public void SetProperties()
    {
        ShopItems.StandardSkin.ShopItem.Initialize(StandardItem.ButtonText, StandardItem.Button, StandardItem.CoinSprite);
        ShopItems.BlueSkin.ShopItem.Initialize(BlueItem.ButtonText, BlueItem.Button, BlueItem.CoinSprite);
        ShopItems.GirlSkin.ShopItem.Initialize(GirlItem.ButtonText, GirlItem.Button, GirlItem.CoinSprite);

        ShopItems.Stone.ShopItem.Initialize(StoneItem.ButtonText, StoneItem.Button, StoneItem.CoinSprite);
        ShopItems.Dart.ShopItem.Initialize(DartItem.ButtonText, DartItem.Button, DartItem.CoinSprite);
        ShopItems.Coconut.ShopItem.Initialize(CoconutItem.ButtonText, CoconutItem.Button, CoconutItem.CoinSprite);
        ShopItems.Bomb.ShopItem.Initialize(BombItem.ButtonText, BombItem.Button, BombItem.CoinSprite);
    }
}
