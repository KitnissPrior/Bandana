using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGunController : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject AnotherController;
    public int TimeToLeave = 5;

    private string _characterTag = "Character";

    private IEnumerator StartControllers()
    {
        yield return new WaitForSeconds(TimeToLeave);

        gameObject.SetActive(true);
        AnotherController.gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _characterTag)
        {
            gameObject.SetActive(false);

            if (!ArrowGun.EnteredArrowArea)
            {
                ArrowGun.EnteredArrowArea = true;
                ArrowGun.StartFire();
            }
            else
            {
                ArrowGun.EnteredArrowArea = false;
                ArrowGun.StopFire();
                StartCoroutine(StartControllers());
            }
        }
    }
}
