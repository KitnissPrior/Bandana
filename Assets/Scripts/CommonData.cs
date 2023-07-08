using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static string NextScene = "Levels";
    public static int MoneyCount => _moneyCount;
    public static Dictionary<int,int> LevelsMoney = new Dictionary<int,int>();
    public static int CurrentLevel = 1;

    private static int _moneyCount = 0;

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
}
