using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Game : MonoBehaviour
{
    public CharacterSpawner CharacterSpawner;
    public float CharacterXToSave;
    public float CharacterYToSave;
    public float CharacterZToSave;

    private string _fileName = "/SavedData.save";
    private Vector3 _characterPosition;
    private Character _character;
    private Vector3 _defaultPosition = new Vector3(-2.25f, -4.48f, 0f);
    private CommonData _commonData;

    void Start()
    {
        _characterPosition = _defaultPosition;
        _commonData = new CommonData();
    }

    public void Initialize(Character character)
    {
        _character = character;
    }

    private void GetPositions()
    {
        CharacterXToSave = _character.transform.position.x;
        Debug.Log(_character.transform.position.x);
        CharacterYToSave = _character.transform.position.y;
        CharacterZToSave = _character.transform.position.z;
    }

    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();
        GetPositions();

        save.SavedCharacterX = CharacterXToSave;
        save.SavedCharacterY = CharacterYToSave;
        save.SavedCharacterZ = CharacterZToSave;

        return save;
    }

    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(_fileName);

        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game data saved");

    }

    public Vector3 LoadGame()
    {
        if (File.Exists(_fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_fileName, FileMode.Open);

            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            _characterPosition = new Vector3(save.SavedCharacterX, save.SavedCharacterY, save.SavedCharacterZ);

            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");

        return _characterPosition;
    }

    public void ResetGame()
    {
        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);
            _characterPosition = _defaultPosition;
            _commonData.ResetHP();

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}
