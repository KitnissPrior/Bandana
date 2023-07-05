using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int Cost = 1;
    public int MaxCost => _maxCost;
    public int MinCost => _minCost;

    private int _maxCost = 50;
    private int _minCost = 3;

    public void SetRandomCost()
    {
        Cost = Random.Range(MinCost, MaxCost);
    }
}
