using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBind : MonoBehaviour
{
    [SerializeField] private float _timeLeft;

    void Initialize(float timeLeft)
    {
        _timeLeft = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft -= Time.deltaTime;
        /*if (timeLeft < 0)
        {
                //что-то сделать по окончанию времени
        }*/
    }
}
