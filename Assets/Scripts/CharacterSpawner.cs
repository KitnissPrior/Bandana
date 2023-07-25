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
    public List<int> DeadEnemyIds => _deadEnemyIds;

    private Character _character;
    private Vector3 _position;
    private List<int> _deadEnemyIds;

    void LoadGame()
    {
        GameSettings settings = SavingController.LoadGame(CommonData.ShouldResetGame);

        _deadEnemyIds = settings.DeadEnemyIds;
        _position = settings.CharacterPosition;
    }

    void DeleteKilledEnemies()
    {
        foreach (var id in _deadEnemyIds)
            Destroy(Enemies[id].gameObject);
    }

    void Start()
    {
        LoadGame();
        DeleteKilledEnemies();

        _character = Instantiate(CharacterPrefab, _position, transform.rotation);
        _character.Initialize(CharacterData, HealthView, Inventory, BindingBar, ShieldBar, 
            CommonData, SavingController);
        Instantiate(CharacterData.Graphics, _character.transform);
        Camera.Follow = _character.transform;
        SavingController.Initialize(_character);
        if (CommonData.CharacterData == null) CommonData.CharacterData = CharacterData;

    }


    void Update()
    {
        if(_character != null)
        {
            for(int i = 0; i < Enemies.Length; i++)
            {
                if(Enemies[i] != null)
                    Enemies[i].Target = _character.transform;
                else if(!_deadEnemyIds.Contains(i)) 
                    _deadEnemyIds.Add(i);
            }
        }
    }

}
