using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public int HP;
    public float BlinkingDelay = 0.4f;

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

    private IEnumerator HideHP(int index)
    {
        yield return new WaitForSeconds(BlinkingDelay);

        //_healthImages[index].GetComponent<Animator>().SetBool("isDamaged", false);
        _healthImages[index].gameObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < _maxHealth; i++)
        {
            if(i < HP)
            {
                _healthImages[i].sprite = _healthSprite;
                _healthImages[i].GetComponent<Animator>().SetBool("isDamaged", false);
                /*if (!_healthImages[i].gameObject.activeSelf)
                {
                    
                    _healthImages[i].gameObject.SetActive(true);

                    _healthImages[i].GetComponent<Animator>().SetBool("isDamaged", false);
                    Debug.Log($"{i} {_healthImages[i].gameObject.activeSelf}");
                    
                }*/
            }
            else
            {
                //��������� ��������
                _healthImages[i].GetComponent<Animator>().SetBool("isDamaged", true);
                //StartCoroutine(HideHP(i));
            }
        }
    }


}
