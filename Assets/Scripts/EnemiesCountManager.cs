using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesCountManager : MonoBehaviour
{
    public Enemy[] Enemies;
    public int EnemiesCount = 2;

    [SerializeField] private string _goodEndScene;

    private void Start()
    {
        EnemiesCount = Enemies.Length;
    }

    void Update()
    {
        if (EnemiesCount == 0)
        {
            SceneManager.LoadScene(_goodEndScene);
        }
    }
}
