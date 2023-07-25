using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float SavedCharacterX;
    public float SavedCharacterY;
    public float SavedCharacterZ;

    public List<int> SavedDeadEnemyIds;
    public List<int> SavedBonusIds;
    public List<int> SavedGainedMoneyIds;
    public List<int> SavedOpenedChestIds;
    public List<int> SavedCrystalsIds;
}