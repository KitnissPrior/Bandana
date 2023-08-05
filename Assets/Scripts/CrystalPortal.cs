using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrystalPortal : MonoBehaviour
{
    public CommonData CommonData;
    public string BossScene;
    public GameObject Message;

    void Start()
    {
        Message.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if(CommonData.CrystalsCount >= CommonData.MaxCrystalsCount)
            {
                character.SavingController.ResetGame();
                SceneManager.LoadScene(BossScene);
            }
            else
                Message.SetActive(true);
        }
    }
}
