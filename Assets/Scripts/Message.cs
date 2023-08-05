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
        "Ты попал в логово котов — настоящий лабиринт, полный опасностей.\n"+
        "Чтобы выбраться, тебе предстоит пройти его, обходя ловушки, собирая бонусы и сражаясь с котами.",
        "Чтобы управлять Мышонком, используй стрелки или клавиши W, A, S, D.",
        "Количество собранных бонусов ты можешь увидеть в инвентаре справа.\nКстати, о бонусах. Посмотри, какой сундук! Откроем его?",
    };

    private string[] _takeShieldMessages =
    {
        "Тебе выпал щит. Он сможет ненадолго защитить тебя от ловушек и врагов, но при контакте с кошачьим клубком щит разрушится.",
        "Чтобы использовать щит, кликни на соответствующую картинку в инвентаре справа или нажми клавишу \"3\"",
    };

    private string[] _arrowButtonMessages =
    {
        "Чтобы отключить стрелы, нажми на красную кнопку.",
    };

    private string[] _eatCheeseMessages =
    {
        "Вкусный сыр для восстановления здоровья.\n"+
        "Нажми на картинку с сыром в инвентаре или клавишу \"2\", если нужно пополнить здоровье.",
    };

    private string[] _takeScissorsMessages =
    {
        "Ножницы нужны, чтобы разрезать клубок ниток."
    };

    private string[] _cutClewMessages =
    {
        "Клубок парализует тебя на какое-то время.\n"+
        "Нажми на клавишу \"1\" или картинку ножниц в инвентаре, чтобы разрезать нитки.",
    };

    private string[] _fightMessages =
    {
        "В этом коридоре притаился кот.\n"+
        "Будь готов к атаке. Чтобы бросить камень, кликни по любой точке экрана.\n"+
        "Удачи тебе, Мышонок!",
    };

    private string[] _bossMessages =
    {
        "Ты подошел к комнате босса. За ней находится выход наружу.\n"+
        "Но чтобы открыть последнюю дверь, надо сразиться с боссом и получить его ключ.\n"+
        "Будь осторожен! Как только ты войдешь в следующую комнату, все двери в ней закроются и назад пути не будет!\n"+
        "Желаем удачи!",
    };

    private string[] _jungleMessages =
    {
        "Зловещее место... Если хочешь выбраться отсюда живым, найди магический барьер.\n"+ 
        "Он откроется и пропустит тебя дальше, если зарядишь его силой 20 кристаллов. Кристаллы спрятаны где-то в джунглях...",
    };

    private string[] _bridgeMessages =
    {
        "Чтобы перейти на другую сторону, нажми на кнопку.\n"+
        "Будь осторожен! Если упадешь, лишишься всех жизней разом!"
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
