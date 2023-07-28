using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCloser : MonoBehaviour
{
    public GameObject Item;

    public void Close()
    {
        Item.SetActive(false);
    }

    public void Open()
    {
        Item.SetActive(true);
    }
}
