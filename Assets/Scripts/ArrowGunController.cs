using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGunController : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public GameObject AnotherController;
    public int TimeToLeave = 2;
    public Bullet Knife;

    private string _characterTag = "Character";

    private IEnumerator StartControllers()
    {
        yield return new WaitForSeconds(TimeToLeave);

        gameObject.GetComponent<Collider2D>().enabled = true;
        AnotherController.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == _characterTag)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;

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