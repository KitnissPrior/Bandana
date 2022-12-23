using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFound : MonoBehaviour
{
    public string GoodEndScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(GoodEndScene);
    }
}