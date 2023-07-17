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

    void Start()
    {
        _characterPosition = new Vector3(9.36f, -88.65f, 0f);
        _character = CharacterSpawner.CharacterPrefab;
    }

    private void GetPositions()
    {
        CharacterXToSave = _character.transform.position.x;
        Debug.Log(CharacterXToSave);
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
        FileStream file = File.Create(Application.persistentDataPath + _fileName);

        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game data saved");
    }

    public Vector3 LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + _fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + _fileName, FileMode.Open);

            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            //CharacterSpawner.transform.position = new Vector3(save.SavedCharacterX, save.SavedCharacterY, save.SavedCharacterZ);
            _characterPosition = new Vector3(save.SavedCharacterX, save.SavedCharacterY, save.SavedCharacterZ);

            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");

        return _characterPosition;
    }

    public void ResetGame()
    {
        if (File.Exists(Application.persistentDataPath + _fileName))
        {
            File.Delete(Application.persistentDataPath + _fileName);
            GetPositions();

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}
