using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public CommonData CommonData;
    public Slider LoadingBar;

    private IEnumerator ShowNextScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(CommonData.NextScene);
        yield return null;
    }

    private void Start()
    {
        StartCoroutine(ShowNextScene());
    }
}
