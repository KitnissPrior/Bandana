using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public Vector3 CharacterPosition;
    public List<int> DeadEnemyIds;
    public List<int> GainedMoneyIds;
    public List<int> OpenedChestIds;
    public List<int> CrystalsIds;

    //ДЛЯ ЧИТЕРОВ
    //спавнится у выхода из замка:
    private Vector3 _position1 = new Vector3(-10.6f, -98f, 0f);
    //спавнится у выхода из джунглей:
    //private Vector3 _position2 = new Vector3(22f, -100f, 0f);

    //private Vector3 _position1 = new Vector3(-2.25f, -4.48f, 0f);
    private Vector3 _position2 = new Vector3(6.32f, -0.05f, 0f);


    public void Initialize()
    {
        if (CommonData.CurrentLevel == 1)
            CharacterPosition = _position1;
        else
            CharacterPosition = _position2;

        DeadEnemyIds = new List<int>();
        GainedMoneyIds = new List<int>();
        OpenedChestIds = new List<int>();
        CrystalsIds = new List<int>();
    }
}
