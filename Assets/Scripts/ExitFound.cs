using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFound : MonoBehaviour
{
    public string GoodEndScene;
    public CommonData CommonData;
    public Game Game;

    private string _characterTag = "Character";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Character")
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
}
