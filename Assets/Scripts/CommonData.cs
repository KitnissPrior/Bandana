using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static string NextScene = "Levels";
    public static Dictionary<int,int> LevelsMoney = new Dictionary<int,int>();

    public static int HP => _hp;
    public static CharacterData CharacterData;

    public static int MoneyCount => _moneyCount;
    public static int ScissorsCount => _scissorsCount;
    public static int CheeseCount => _cheeseCount;
    public static int ShieldsCount => _shieldsCount;

    public static int CurrentLevel = 1;
    public static bool IsFirstLevelPassed = false;

    private static int _defaultHP = 5;
    private static int _hp = _defaultHP;
    private static int _moneyCount = 0;
    private static int _scissorsCount = 0;
    private static int _cheeseCount = 0;
    private static int _shieldsCount = 0;

    private void Start()
    {
        LevelsMoney[1] = 30;
        LevelsMoney[2] = 50;
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

    public void SetHP(int hp)
    {
        _hp += hp;
    }

    public void ResetHP()
    {
        _hp = _defaultHP;
    }
}
