using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public GameObject MessageWindow;
    public TMP_Text CurrentText;

    public enum MessageType
    {
        Start,
        TakeShield,
        Boss
    };

    public MessageType CurrentType;

    public Dictionary<MessageType, string[]> Messages;

    private int _clickCount = 0;

    private string[] _startMessages =
    {
        "�� ����� � ������ ����� � ��������� ��������, ������ ����������.\n"+
        "����� ���������, ���� ��������� ������ ���, ������ �������, ������� ������ � �������� � ������.",
        "����� ��������� ��������, ��������� ������� ��� ������� W, A, S, D",
        "���������� ��������� ������� �� ������ ������� � ��������� ������.\n������ � �������. ��������, ����� ������! ������� ���?",
    };

    private string[] _takeShieldMessages =
    {
        "���� ����� ���. �� ������ ��������� �������� ���� �� ������� � ������, �� ��� �������� � �������� ������� ��� ����������.",
        "����� ������������ ���, ������ �� ��������������� �������� � ��������� ������ ��� ����� ������� \"3\""
    };

    private string[] _bossMessages =
    {
        "�� ������� � ������� �����. �� ��� ��������� ����� ������.\n"+
        "�� ����� ������� ��������� �����, ���� ��������� � ������ � �������� ��� ����.\n"+
        "���� ���������! ��� ������ �� ������� � ��������� �������, ��� ����� � ��� ��������� � ����� ���� �� �����!\n"+
        "������ �����!",
    };

    private void FillMessageDictionary()
    {
        Messages = new Dictionary<MessageType, string[]>();

        Messages.Add(MessageType.Start, _startMessages);
        Messages.Add(MessageType.TakeShield, _takeShieldMessages);
        Messages.Add(MessageType.Boss, _bossMessages);
    }

    void Start()
    {
        MessageWindow.SetActive(false);

        FillMessageDictionary();
    }

    private void CloseMessage()
    {
        MessageWindow.SetActive(false);
        ResumeGame();
    }

    public void OnButtonClick()
    {
        _clickCount++;

        if (_clickCount < Messages[CurrentType].Length)
        {
            CurrentText.text = Messages[CurrentType][_clickCount];
        }
        else
        {
            CloseMessage();
        }
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
