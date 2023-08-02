using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public static CharacterData StandardSkin;
    public static CharacterData BlueSkin;
    public static CharacterData GirlSkin;
    public static List<CharacterData> Skins;
    public static CharacterBullet Stone;
    public static CharacterBullet Dart;
    public static CharacterBullet Coconut;
    public static CharacterBullet Bomb;
    public static List<CharacterBullet> Weapons;

    public void SetItems()
    {
        Skins = new List<CharacterData>() { StandardSkin, BlueSkin, GirlSkin };
        Weapons = new List<CharacterBullet>() { Stone, Dart, Coconut, Bomb };
        if (CommonData.CharacterData == null)
            CommonData.CharacterData = StandardSkin;
    }

    public void Reset()
    {
        if (StandardSkin != null) StandardSkin.IsActive = true;
        foreach (var skin in Skins)
        {
            if (skin != StandardSkin && skin != null)
            {
                skin.IsActive = false;
                skin.IsInStock = false;
            }
        }

        foreach (var weapon in Weapons)
        {
            if(Stone != null) Stone.IsActive = true;
            if(weapon != Stone && weapon != null)
            {
                weapon.IsActive = false;
                weapon.IsInStock = false;
            }
        }
    }
}
