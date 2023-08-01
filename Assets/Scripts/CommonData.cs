using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CommonData : MonoBehaviour
{
    public static string NextScene = "Levels";
    public static Dictionary<int,int> LevelsMoney = new Dictionary<int,int>();

    public static int HP => _hp;
    public static CharacterData CharacterData;
    public static CharacterData StandardSkin;
    public static CharacterData BlueSkin;
    public static CharacterData GirlSkin;
    public static List<CharacterData> Skins;

    public static int MoneyCount => _moneyCount;
    public static int ScissorsCount => _scissorsCount;
    public static int CheeseCount => _cheeseCount;
    public static int ShieldsCount => _shieldsCount;
    public static int CrystalsCount => _crystalsCount;
    public static int MaxCrystalsCount => _maxCrystalsCount;

    public static int CurrentLevel = 1;
    public static bool IsFirstLevelPassed = false;
    public static bool ShouldResetGame => _shouldResetGame;
    public static string SavedDataFile => _savedDataFile;

    private static int _defaultHP = 5;
    private static int _hp = _defaultHP;
    private static int _moneyCount = 0;
    private static int _scissorsCount = 0;
    private static int _cheeseCount = 0;
    private static int _shieldsCount = 0;
    private static int _crystalsCount = 0;
    private static int _maxCrystalsCount = 20;
    private static bool _shouldResetGame = false;
    private static string _savedDataFile = "/SavedData.save";

    private void Start()
    {
        LevelsMoney[1] = 30;
        LevelsMoney[2] = 50;
        /*Skins = new List<CharacterData>();
        Skins.Add(StandardSkin);
        Skins.Add(BlueSkin);
        Skins.Add(GirlSkin);*/
        CharacterData = StandardSkin;
    }

    public bool IsSavedData() => File.Exists(_savedDataFile);

    public void ResetValues()
    {
        ReduseMoney(MoneyCount);
        SetCheeseCount(-CheeseCount);
        SetScissorsCount(-ScissorsCount);
        SetShieldsCount(-ShieldsCount);
        SetCrystalsCount(-CrystalsCount);

        IsFirstLevelPassed = false;
        CurrentLevel = 1;
        _shouldResetGame = true;
        NextScene = "Levels";

        if(StandardSkin != null) StandardSkin.IsActive = true;
        BlueSkin.IsInStock = false;
        BlueSkin.IsActive = false;
        GirlSkin.IsInStock = false;
        GirlSkin.IsActive = false;
}

    public void NotResetGame()
    {
        _shouldResetGame = false;
    }

    public void AddMoney(int money)
    {
        _moneyCount += money;
    }

    public void ReduseMoney(int money)
    {
        _moneyCount -= money;
    }

    public void SetCurrentLevel(int level)
    {
        CurrentLevel = level;
    }

    public void SetCheeseCount(int count)
    {
        _cheeseCount += count;
        if (count < 0) SetHP(-count);
    }

    public void SetShieldsCount(int count)
    {
        _shieldsCount += count;
    }

    public void SetScissorsCount(int count)
    {
        _scissorsCount += count;
    }

    public void SetCrystalsCount(int count)
    {
        _crystalsCount += count;
    }

    public void SetHP(int hp)
    {
        _hp += hp;
    }

    public void ResetHP()
    {
        _hp = _defaultHP;
    }
}
