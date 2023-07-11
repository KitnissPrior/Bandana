using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int Cost = 1;
    public int MinCost = 3;
    public CommonData CommonData;

    public void SetRandomCost()
    {
        int level = CommonData.CurrentLevel;
        Cost = Random.Range(MinCost, CommonData.LevelsMoney[level]);
    }
}
