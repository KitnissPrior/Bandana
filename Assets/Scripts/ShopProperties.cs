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
        if (ShopItems.StandardSkin.ShopItem == null)
            ShopItems.StandardSkin.ShopItem = StandardItem;
        if (ShopItems.BlueSkin.ShopItem == null)
            ShopItems.BlueSkin.ShopItem = BlueItem;
        if (ShopItems.GirlSkin.ShopItem == null)
            ShopItems.GirlSkin.ShopItem = GirlItem;
        if (ShopItems.Stone.ShopItem == null)
            ShopItems.Stone.ShopItem = StoneItem;
        if (ShopItems.Dart.ShopItem == null)
            ShopItems.Dart.ShopItem = DartItem;
        if (ShopItems.Coconut.ShopItem == null)
            ShopItems.Coconut.ShopItem = CoconutItem;
        if (ShopItems.Bomb.ShopItem == null)
            ShopItems.Bomb.ShopItem = BombItem;
    }
}
