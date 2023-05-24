using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    public GameObject MessageWindow;

    void Start()
    {
        MessageWindow.SetActive(false);
    }

    public void CloseMessage()
    {
        MessageWindow.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            PauseGame();
            MessageWindow.SetActive(true);
        }

        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}
