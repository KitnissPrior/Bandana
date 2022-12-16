using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Character CharacterPrefab;
    public CharacterData CharacterData;
    public HealthView HealthView;

    void Start()
    {
        Character character = Instantiate(CharacterPrefab, transform.position, transform.rotation);
        character.Initialize(CharacterData, HealthView);
        Instantiate(CharacterData.Graphics, character.transform);
    }

}
