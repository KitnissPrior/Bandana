using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public Vector3 CharacterPosition = new Vector3(-2.25f, -4.48f, 0f);
    public List<int> DeadEnemyIds = new List<int>();
    public List<int> GainedMoneyIds = new List<int>();
    public List<int> OpenedChestIds = new List<int>();
    public List<int> ClosedMessageIds = new List<int>();
}
