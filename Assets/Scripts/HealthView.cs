using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public int HP;

    [SerializeField] private CharacterData _characterData;
    [SerializeField] private Image[] _healthImages;
    [SerializeField] private Sprite _healthSprite;
    [SerializeField] private Character _character;

    private int _maxHealth;

    private void Start()
    {
        HP = _characterData.HP;
        _maxHealth = HP;
    }

    private void Update()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            if(i < HP)
            {
                _healthImages[i].sprite = _healthSprite;

                if (!_healthImages[i].gameObject.activeSelf)
                {
                    _healthImages[i].gameObject.SetActive(true);

                }
            }
            else
            {
                //скрываем одну жизнь
                _healthImages[i].gameObject.SetActive(false);
            }
        }
    }


}
