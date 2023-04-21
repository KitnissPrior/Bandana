using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindingBar : MonoBehaviour
{
    public Image Bar;
    public Image Background;
    public float Value;

    void Start()
    {
        Value = 0f;
        Background.enabled = false;
    }

    void Update()
    {
        Bar.fillAmount = Value;
    }

    public void ReduceTimeLeft(float value)
    {
        Value -= value;
        if(Value >= 0.02f) Background.enabled = true;
        else Background.enabled = false;
        Update();
    }
}
