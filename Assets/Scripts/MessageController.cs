using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public Message Message;

    private string _characterTag = "Character";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _characterTag)
        {
            if(Message) Destroy(Message.gameObject);
        }
    }
}
