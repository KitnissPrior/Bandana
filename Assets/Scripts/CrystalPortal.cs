using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalPortal : MonoBehaviour
{
    public CommonData CommonData;
    public string BossScene;
    public GameObject Message;

    private string _characterTag = "Character";

    void Start()
    {
        Message.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _characterTag)
        {
            if(CommonData.CrystalsCount >= CommonData.MaxCrystalsCount)
                SceneManager.LoadScene(BossScene);
            else
                Message.SetActive(true);
        }
    }
}
