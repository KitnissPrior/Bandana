using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    public static string NextScene = "Levels";
    public static int MoneyCount => _moneyCount;

    private static int _moneyCount = 0;

    public void AddMoney(int money)
    {
        _moneyCount += money;
    }

    public void ReduseMoney(int money)
    {
        _moneyCount -= money;
    }
}
