using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public CharacterData StandardSkin;
    public CharacterData BlueSkin;
    public CharacterData GirlSkin;
    public CharacterBullet Stone;
    public CharacterBullet Dart;
    public CharacterBullet Coconut;
    public CharacterBullet Bomb;
    public ShopItems ShopItems;

    void Start()
    {
        if (ShopItems.StandardSkin == null)
            ShopItems.StandardSkin = StandardSkin;
        if (ShopItems.BlueSkin == null)
            ShopItems.BlueSkin = BlueSkin;
        if (ShopItems.GirlSkin == null)
            ShopItems.GirlSkin = GirlSkin;
        if (ShopItems.Stone == null)
            ShopItems.Stone = Stone;
        if (ShopItems.Dart == null)
            ShopItems.Dart = Dart;
        if (ShopItems.Coconut == null)
            ShopItems.Coconut = Coconut;
        if (ShopItems.Bomb == null)
            ShopItems.Bomb = Bomb;
        if (CommonData.ShopItems == null)
            CommonData.ShopItems = ShopItems;
    }
}
