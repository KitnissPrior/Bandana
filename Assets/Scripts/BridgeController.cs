using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public GameObject Bridge;
    public Trap Chasm;

    public ProgressBar ProgressBar;
    public int Delay = 4;

    private string _characterTag = "Character";
    private int _barSmoothingCoeff = 5;
    private Collider2D _collider;
    private int _maxDamage;

    void Start()
    {
        Bridge.SetActive(false);
        _collider = GetComponent<Collider2D>();
        _maxDamage = Chasm.Damage;
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

    void ShowBridge()
    {
        Bridge.SetActive(true);
        _collider.enabled = false;
        Chasm.Damage = 0;
    }

    void HideBridge()
    {
        Bridge.SetActive(false);
        _collider.enabled = true;
        Chasm.Damage = _maxDamage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == _characterTag)
        {
            if (!Bridge.gameObject.activeSelf)
            {
                ShowBridge();
                ProgressBar.Value = 1f;
                StartCoroutine(UpdateProgressBar());
                Invoke("HideBridge", Delay);
            }
        }
    }
}
