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
    public BindingBar BindingBar;

    void Start()
    {
        Character character = Instantiate(CharacterPrefab, transform.position, transform.rotation);
        character.Initialize(CharacterData, HealthView, Inventory, BindingBar);
        Instantiate(CharacterData.Graphics, character.transform);
        Camera.Follow = character.transform;

        foreach (Enemy enemy in Enemies)
        {
            enemy.Target = character.transform;
        }
        
    }

}
