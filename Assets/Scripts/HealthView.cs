using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public float BlinkingDelay = 0.4f;
    public CommonData CommonData;

    [SerializeField] private CharacterData _characterData;    
    [SerializeField] private Image[] _healthImages;
    [SerializeField] private Sprite _healthSprite;
    [SerializeField] private Character _character;

    private int _maxHealth = 5;

    private void Start()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            if (i < CommonData.HP)
            {
                _healthImages[i].sprite = _healthSprite;

                if (!_healthImages[i].gameObject.activeSelf)
                {
                    _healthImages[i].gameObject.SetActive(true);

                }
            }
            else
            {
                _healthImages[i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator HideHP(int index)
    {
        yield return new WaitForSeconds(BlinkingDelay);

        _healthImages[index].gameObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            if(i < CommonData.HP)
            {
                _healthImages[i].sprite = _healthSprite;
                _healthImages[i].GetComponent<Animator>().SetBool("isDamaged", false);
            }
            else
            {
                //запускаем мерцание
                _healthImages[i].GetComponent<Animator>().SetBool("isDamaged", true);
            }
        }
    }


}
