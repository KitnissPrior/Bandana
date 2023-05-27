using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;

public class CheeseGenerator : MonoBehaviour
{
    public GameObject CheesePrefab;
    public GameObject[] Points;

    private int _minCount = 1;
    private int _maxCount;

    private List<int> usedPoints = new List<int>();

    void Start()
    {
        _maxCount = Points.Length;
        int cheeseCount = Random.Range(_minCount, _maxCount);

        while(usedPoints.Count < cheeseCount)
        {
            int pointId = Random.Range(0, _maxCount - 1);
            if (!usedPoints.Contains(pointId))
            { 
                usedPoints.Add(pointId);
                Instantiate(CheesePrefab, Points[pointId].transform.position, Quaternion.identity);
            }
        }
    }
}
