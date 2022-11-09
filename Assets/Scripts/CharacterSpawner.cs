using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Character CharacterPrefab;
    public CharacterData CharacterData;

    // Start is called before the first frame update
    void Start()
    {
        Character character = Instantiate(CharacterPrefab, transform.position, transform.rotation);
        character.Initialize(CharacterData);
        Instantiate(CharacterData.Graphics, character.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
