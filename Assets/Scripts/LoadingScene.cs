using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public string NextScene;
    public Slider LoadingBar;

    private IEnumerator ShowNextScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(NextScene);
        /*while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadingBar.value = progress;
            yield return null;
        }*/
        yield return null;
    }

    private void Start()
    {
        StartCoroutine(ShowNextScene());
    }
}
