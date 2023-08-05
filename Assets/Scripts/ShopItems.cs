using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public static CharacterData StandardSkin;
    public static CharacterData BlueSkin;
    public static CharacterData GirlSkin;

    public static CharacterBullet Stone;
    public static CharacterBullet Dart;
    public static CharacterBullet Coconut;
    public static CharacterBullet Bomb;

    public static List<ShopItem> Skins;
    public static List<ShopItem> Weapons;

    public void SetItems()
    {
        if (Skins == null || StandardSkin.ShopItem == null)
        {
            Skins = new List<ShopItem>() { StandardSkin.ShopItem, BlueSkin.ShopItem, GirlSkin.ShopItem };
        }
        if(Weapons == null || Stone.ShopItem == null)
            Weapons = new List<ShopItem>() { Stone.ShopItem, Dart.ShopItem, Coconut.ShopItem, Bomb.ShopItem };
        if (CommonData.CharacterData == null)
            CommonData.CharacterData = StandardSkin;
    }

    public void Reset()
    {
        if (StandardSkin != null) StandardSkin.ShopItem.IsActive = true;
        foreach (var skin in Skins)
        {
            if (skin != null && skin.Name != StandardSkin.ShopItem.Name)
            {
                skin.IsActive = false;
                skin.IsInStock = false;
            }
        }

        if (Stone != null) Stone.ShopItem.IsActive = true;
        foreach (var weapon in Weapons)
        {
            if(weapon != null && weapon.Name != Stone.ShopItem.Name)
            {
                weapon.IsActive = false;
                weapon.IsInStock = false;
            }
        }
    }
}
