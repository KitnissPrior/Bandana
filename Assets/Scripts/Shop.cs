using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TMP_Text MoneyText;
    public CommonData CommonData;

    void Start()
    {
        MoneyText.text = $"{CommonData.MoneyCount}";
    }
}
