using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public bool IsActive;
    public int ProtectingTime = 10;
    public GameObject Bullet;
    public GameObject Cheese;

    void Start()
    {
        IsActive = false;
        Physics2D.IgnoreCollision(Bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Cheese.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void Update()
    {
        if (IsActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Clew")
        {
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.TryGetComponent<CharacterBullet>(out CharacterBullet bullet))
        {
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
