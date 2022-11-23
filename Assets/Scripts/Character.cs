using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public PlayerRun PlayerRun;
    public Health Health;
    public Gun Gun;
    public Transform GunPosition;
    public CharacterData CharacterData;

    internal void Initialize(CharacterData characterData)
    {
        CharacterData = characterData;
        Health.Initialize(CharacterData.HP);
        PlayerRun.Initialize(CharacterData.Speed);
        Gun = Instantiate(CharacterData.Gun, GunPosition);
    }
}
