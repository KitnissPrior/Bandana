using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float SavedCharacterX;
    public float SavedCharacterY;
    public float SavedCharacterZ;

    public List<Enemy> SavedEnemies;
    public List<GameObject> SavedBonuses;
    public List<Money> SavedCoins;
}
