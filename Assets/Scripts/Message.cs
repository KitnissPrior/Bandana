using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message : MonoBehaviour
{
    public GameObject MessageWindow;
    public TMP_Text CurrentText;

    public bool IsShort = false;

    public enum MessageType
    {
        Start,
        TakeShield,
        ArrowButton,
        EatCheese,
        TakeScissors,
        CutClew,
        Fight,
        Boss,
        Jungle,
        Bridge
    };

    public MessageType CurrentType;

    public Dictionary<MessageType, string[]> Messages;

    private int _clickCount = 0;
    private float _closingDelay = 2f;

    private Collider2D _detectorCollider;

    private string[] _startMessages =
    {
        "�� ����� � ������ ����� � ��������� ��������, ������ ����������.\n"+
        "����� ���������, ���� ��������� ������ ���, ������ �������, ������� ������ � �������� � ������.",
        "����� ��������� ��������, ��������� ������� ��� ������� W, A, S, D.",
        "���������� ��������� ������� �� ������ ������� � ��������� ������.\n������, � �������. ��������, ����� ������! ������� ���?",
    };

    private string[] _takeShieldMessages =
    {
        "���� ����� ���. �� ������ ��������� �������� ���� �� ������� � ������, �� ��� �������� � �������� ������� ��� ����������.",
        "����� ������������ ���, ������ �� ��������������� �������� � ��������� ������ ��� ����� ������� \"3\"",
    };

    private string[] _arrowButtonMessages =
    {
        "����� ��������� ������, ����� �� ������� ������.",
    };

    private string[] _eatCheeseMessages =
    {
        "������� ��� ��� �������������� ��������.\n"+
        "����� �� �������� � ����� � ��������� ��� ������� \"2\", ���� ����� ��������� ��������.",
    };

    private string[] _takeScissorsMessages =
    {
        "������� �����, ����� ��������� ������ �����."
    };

    private string[] _cutClewMessages =
    {
        "������ ���������� ���� �� �����-�� �����.\n"+
        "����� �� ������� \"1\" ��� �������� ������ � ���������, ����� ��������� �����.",
    };

    private string[] _fightMessages =
    {
        "� ���� �������� ��������� ���.\n"+
        "���� ����� � �����. ����� ������� ������, ������ �� ����� ����� ������.\n"+
        "����� ����, �������!",
    };

    private string[] _bossMessages =
    {
        "�� ������� � ������� �����. �� ��� ��������� ����� ������.\n"+
        "�� ����� ������� ��������� �����, ���� ��������� � ������ � �������� ��� ����.\n"+
        "���� ���������! ��� ������ �� ������� � ��������� �������, ��� ����� � ��� ��������� � ����� ���� �� �����!\n"+
        "������ �����!",
    };

    private string[] _jungleMessages =
    {
        "�������� �����... ���� ������ ��������� ������ �����, ����� ���������� ������.\n"+ 
        "�� ��������� � ��������� ���� ������, ���� �������� ��� ����� 20 ����������. ��������� �������� ���-�� � ��������...",
    };

    private string[] _bridgeMessages =
    {
        "����� ������� �� ������ �������, ����� �� ������.\n"+
        "���� ���������! ���� �������, �������� ���� ������ �����!"
    };

    private void FillMessageDictionary()
    {
        Messages = new Dictionary<MessageType, string[]>();

        Messages.Add(MessageType.Start, _startMessages);
        Messages.Add(MessageType.TakeShield, _takeShieldMessages);
        Messages.Add(MessageType.ArrowButton, _arrowButtonMessages);
        Messages.Add(MessageType.EatCheese, _eatCheeseMessages);
        Messages.Add(MessageType.TakeScissors, _takeScissorsMessages);
        Messages.Add(MessageType.CutClew, _cutClewMessages);
        Messages.Add(MessageType.Fight, _fightMessages);
        Messages.Add(MessageType.Boss, _bossMessages);
        Messages.Add(MessageType.Jungle, _jungleMessages);
        Messages.Add(MessageType.Bridge, _bridgeMessages);
    }

    void Start()
    {
        MessageWindow.SetActive(false);
        _detectorCollider = gameObject.GetComponent<Collider2D>();

        FillMessageDictionary();
    }

    public void CloseMessage()
    {
        Destroy(gameObject);
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
            ResumeGame();
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

    private void CheckIfCharacterCollided(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<Character>(out Character character))
        {
            Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), _detectorCollider);

            if (character.Shield.IsActive)
            {
                Physics2D.IgnoreCollision(character.Shield.GetComponent<Collider2D>(), _detectorCollider);
            }

            MessageWindow.SetActive(true);

            if (!IsShort) PauseGame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfCharacterCollided(collision.gameObject);
    }
}
