using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceChecker : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform);
    }
}
