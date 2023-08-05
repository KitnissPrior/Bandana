using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCloser : MonoBehaviour
{
    public GameObject[] Objects;

    void Start()
    {
        foreach (var obj in Objects)
        {
                obj.SetActive(false);
        }
    }

    public void CloseOtherObjects(GameObject activeObject)
    {
        foreach(var obj in Objects)
        {
            if(obj != activeObject)
                obj.SetActive(false);
        }
    }
}
