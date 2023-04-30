using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSpawner : MonoBehaviour
{
    public Character CharacterPrefab;
    public CharacterData CharacterData;
    public HealthView HealthView;
    public CinemachineVirtualCamera Camera;
    public Enemy[] Enemies;
    public Inventory Inventory;
    public ProgressBar BindingBar;
    public ProgressBar ShieldBar;

    private Character _character;

    void Start()
    {
        _character = Instantiate(CharacterPrefab, transform.position, transform.rotation);
        _character.Initialize(CharacterData, HealthView, Inventory, BindingBar, ShieldBar);
        Instantiate(CharacterData.Graphics, _character.transform);
        Camera.Follow = _character.transform;
    }

    void Update()
    {
        if(_character != null)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Target = _character.transform;
            }
        }
    }

}
