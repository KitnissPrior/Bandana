using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public CommonData CommonData;

    public void OpenMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void OpenScene(string scene)
    {
        if (scene != "Shop")
            CommonData.NextScene = scene;
        SceneManager.LoadScene(scene);
    }

    public void ShowLoading(string nextScene)
    {
        SceneManager.LoadScene("LoadingScene");

        if (nextScene != "")
        {
            CommonData.NextScene = nextScene;
        }
    }

    public void SetCurrentLevel(int level)
    {
        //if (CommonData.CurrentLevel == 2)
        CommonData.CurrentLevel = level;
    }
}
