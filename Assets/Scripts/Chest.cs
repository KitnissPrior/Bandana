using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] Bonuses;
    public Money Money;
    public Transform BonusPoint;
    public float ShowingDelay = 0.4f;
    public Animator Animator;

    public enum BonusTypes
    {
        Scissors,
        Cheese,
        Shield,
        Random,
        Money,
    };

    public BonusTypes BonusType = BonusTypes.Random;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void GetBonus()
    {
        if (BonusType == BonusTypes.Money)
        {
            Money money = Instantiate(Money, BonusPoint.position, BonusPoint.rotation);
            money.SetRandomCost();
        }
        else
        {
            int index = (BonusType == BonusTypes.Random) ? Random.Range(0, Bonuses.Length) : (int)BonusType;
            Instantiate(Bonuses[index], BonusPoint.position, BonusPoint.rotation);
        }
    }

    private IEnumerator ShowBonus()
    {
        yield return new WaitForSeconds(ShowingDelay);

        GetBonus();
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            Animator.SetBool("isShowing", true);

            StartCoroutine(ShowBonus());
        }
    }
}
