using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Character CharacterPrefab;
    public CharacterData CharacterData;

    void Start()
    {
        Character character = Instantiate(CharacterPrefab, transform.position, transform.rotation);
        character.Initialize(CharacterData);
        Instantiate(CharacterData.Graphics, character.transform);
    }

    void Update()
    {

    }
}
