using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 CameraChangePos;
    public Vector3 PlayerChangePos;
    public Camera Cam;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            other.transform.position += PlayerChangePos;
            Cam.transform.position += CameraChangePos;
            Debug.Log("Ошибка");
        }
        Debug.Log('Лог');
    }
}   
