using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public CommonData CommonData;
    public Button Level2Button;
    public GameObject LockPanel;

    void Start()
    {
        Level2Button.interactable = CommonData.IsFirstLevelPassed;
        LockPanel.SetActive(!CommonData.IsFirstLevelPassed);
    }
}
