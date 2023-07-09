using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnTouchDevice : MonoBehaviour
{

    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
