using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFound : MonoBehaviour
{
    public string GoodEndScene;
    public CommonData CommonData;
    public Game Game;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(GoodEndScene);
        Game.ResetGame();
        CommonData.ResetHP();
        if (CommonData.CurrentLevel == 1)
        {
            CommonData.IsFirstLevelPassed = true;
        }
        CommonData.NextScene = "Levels";
    }
}
