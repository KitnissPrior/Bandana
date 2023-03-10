using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class CheeseGenerator : MonoBehaviour
{
    public GameObject CheesePrefab;
    public GameObject[] Points;

    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 4;

    private List<int> usedPoints = new List<int>();

    void Start()
    {
        int cheeseCount = Random.Range(minCount, maxCount);

        while(usedPoints.Count < cheeseCount)
        {
            int pointId = Random.Range(0, Points.Length-1);
            if (!usedPoints.Contains(pointId))
            { 
                usedPoints.Add(pointId);
                Instantiate(CheesePrefab, Points[pointId].transform.position, Quaternion.identity);
            }
        }
    }
}
