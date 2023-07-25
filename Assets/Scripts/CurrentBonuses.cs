using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentBonuses : MonoBehaviour
{
    public Chest[] Chests;
    public Money[] Coins;
    public GameObject[] Crystals;

    public List<int> CheckChests()
    {
        List<int> ids = new List<int>();
        for(int i = 0; i < Chests.Length; i++)
        {
            if(Chests[i] == null)
                ids.Add(i);
        }
        return ids;
    }

    public List<int> CheckCoins()
    {
        List<int> ids = new List<int>();
        for (int i = 0; i < Coins.Length; i++)
        {
            if (Coins[i] == null)
                ids.Add(i);
        }
        return ids;
    }

    public List<int> CheckCrystals()
    {
        List<int> ids = new List<int>();
        for (int i = 0; i < Crystals.Length; i++)
        {
            if (Crystals[i] == null)
                ids.Add(i);
        }
        return ids;
    }
}
