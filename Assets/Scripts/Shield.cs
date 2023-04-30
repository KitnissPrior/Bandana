using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool IsActive;
    public GameObject Knife;
    public GameObject Cheese;
    public GameObject Clew;

    void Start()
    {
        IsActive = false;
        Physics2D.IgnoreCollision(Knife.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Cheese.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(Clew.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsActive);
        if (IsActive)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Trap trap))
    }*/
}
