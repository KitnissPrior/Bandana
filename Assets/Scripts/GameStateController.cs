using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public GameObject Panel;
    public TMP_Text StartButtonText;
    public Button NewGameButton;
    public CommonData CommonData;

    private string _continueText = "Продолжить";
    private string _startText = "Старт";

    void Start()
    {
        Panel.SetActive(false);
        if(CommonData.IsSavedData())
        {
            StartButtonText.text = _continueText;
            if(!NewGameButton.gameObject.activeSelf)
                NewGameButton.gameObject.SetActive(true);
        }
        else
        {
            StartButtonText.text = _startText;
            NewGameButton.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScene");
        if (CommonData.IsFirstLevelPassed) CommonData.NextScene = "Levels";
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);
    }

    public void StartNewGame()
    {
        CommonData.ResetValues();
    }
}
