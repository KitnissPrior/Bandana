using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int cheeseHP = 1;
    [SerializeField] private TMP_Text cheeseText;
    [SerializeField] private TMP_Text healthComment;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject eatButton;

    private Character character;
    private int cheeseCount;

    public void Initialize(Character player)
    {
        character = player;
        cheeseCount = 0;
    }

    void ShowCheeseInfo ()
    {
        cheeseText.text = $"—ыра: {cheeseCount}";

        int HP = character.Health.HitPoints;
        int maxHP = character.Health.MaxHP;

        if (HP == maxHP)
        {
            healthComment.text = "«доровье максимальное";
            eatButton.SetActive(false);
        }
        if (HP < maxHP && cheeseCount == 0)
        {
            healthComment.text = "";
            eatButton.SetActive(false);
        }
        if (HP < maxHP && cheeseCount > 0)
        {
            healthComment.text = "";
            eatButton.SetActive(true);
        }
    }

    public void Update()
    {
        ShowCheeseInfo();
    }

    public void OpenInventory()
    {
        if (!window.activeSelf)
        {
            window.SetActive(true);
        }
    }

    public void CloseInventory()
    {
        window.SetActive(false);
    }

    public void AddCheese()
    {
        cheeseCount++;
    }

    public void EatCheese()
    {
        cheeseCount--;

        character.Health.Heal(cheeseHP);
        character.HealthView.HP += cheeseHP;
    }

}
