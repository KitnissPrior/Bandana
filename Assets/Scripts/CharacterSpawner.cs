using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
    public CommonData CommonData;
    public Game SavingController;

    private Character _character;
    private Vector3 _position;

    void Start()
    {
        _position = SavingController.LoadGame();
        _character = Instantiate(CharacterPrefab, _position, transform.rotation);
        _character.Initialize(CharacterData, HealthView, Inventory, BindingBar, ShieldBar, CommonData);
        Instantiate(CharacterData.Graphics, _character.transform);
        Camera.Follow = _character.transform;
        SavingController.Initialize(_character);
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
