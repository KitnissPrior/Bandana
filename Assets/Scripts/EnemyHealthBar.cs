using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image Bar;
    public Image Background;

    private float _value;

    void Start()
    {
        _value = 1f;
        Bar.enabled = false;
        Background.enabled = false;
    }

    void ShowBar()
    {
        Bar.enabled = true;
        Background.enabled = true;
    }
    
    void Update()
    {
        Bar.fillAmount = _value;
    }

    public void ReduceValue(float value)
    {
        if (!Bar.enabled) ShowBar();
        _value -= value;
        Update();
    }

}
