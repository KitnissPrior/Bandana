using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public CommonData CommonData;
    public CharacterData StandardSkin;
    public CharacterData BlueSkin;
    public CharacterData GirlSkin;
    
    public void SetItems()
    {
        if(CommonData.StandardSkin == null)
            CommonData.StandardSkin = StandardSkin;
        if (CommonData.BlueSkin == null)
            CommonData.BlueSkin = BlueSkin;
        if (CommonData.GirlSkin == null)
            CommonData.GirlSkin = GirlSkin;

    }
}
