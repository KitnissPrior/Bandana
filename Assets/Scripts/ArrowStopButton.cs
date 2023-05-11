using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowStopButton : MonoBehaviour
{
    public ArrowGun ArrowGun;
    public ProgressBar ProgressBar;
    public int Delay = 5;

    private string _characterTag = "Character";
    private int _barSmoothingCoeff = 5;

    void Start()
    {
        StartFire();
        ArrowGun.Update();
    }

    public IEnumerator UpdateProgressBar()
    {
        int count = _barSmoothingCoeff * Delay;

        for (int i = 0; i < count; i++)
        {
            ProgressBar.ReduceTimeLeft(1f / count);
            yield return new WaitForSeconds(1f / _barSmoothingCoeff);
        }
    }

    void StartFire()
    {
        ArrowGun.StartFire();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == _characterTag)
        {
            if(ArrowGun.IsActive)
            {
                ArrowGun.StopFire();

                //StartCoroutine(UpdateProgressBar());
                Invoke("StartFire", Delay);
            }
        }
    }
}
