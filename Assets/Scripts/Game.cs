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
    public List<int> DeadEnemyIdsToSave;
    public List<int> OpenedChestIdsToSave;
    public List<int> GainedMoneyIdsToSave;
    public CurrentBonuses CurrentBonuses;

    private string _fileName = "/SavedData.dat";
    private Vector3 _characterPosition;
    private Character _character;
    private GameSettings _settings;

    void Start()
    {
        _settings = new GameSettings();
        DeadEnemyIdsToSave = new List<int>();
        OpenedChestIdsToSave = new List<int>();
        GainedMoneyIdsToSave = new List<int>();
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

    public void DeleteGainedBonuses()
    {
        foreach (var id in _settings.OpenedChestIds)
            Destroy(CurrentBonuses.Chests[id].gameObject);
        foreach (var id in _settings.GainedMoneyIds)
            Destroy(CurrentBonuses.Coins[id].gameObject);
    }

    public List<int> GetDeadEnemies()
    {
        DeadEnemyIdsToSave = new List<int>(CharacterSpawner.DeadEnemyIds);
        return DeadEnemyIdsToSave;
    }

    public List<int> GetOpenedChests()
    {
        OpenedChestIdsToSave = CurrentBonuses.CheckChests();
        return OpenedChestIdsToSave;
    }

    public List<int> GetGainedMoney()
    {
        GainedMoneyIdsToSave = CurrentBonuses.CheckCoins();
        return GainedMoneyIdsToSave;
    }

    public void OpenChest(int id)
    {
        if(!_settings.OpenedChestIds.Contains(id))
            _settings.OpenedChestIds.Add(id);
    }

    public void GainMoney(int id)
    {
        if (!_settings.GainedMoneyIds.Contains(id))
            _settings.GainedMoneyIds.Add(id);
    }

    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();
        GetPositions();

        save.SavedCharacterX = CharacterXToSave;
        save.SavedCharacterY = CharacterYToSave;
        save.SavedCharacterZ = CharacterZToSave;
        save.SavedDeadEnemyIds = GetDeadEnemies();
        save.SavedGainedMoneyIds = GetGainedMoney();
        save.SavedOpenedChestIds = GetOpenedChests();

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

    public GameSettings LoadGame()
    {
        if (File.Exists(_fileName))
        {
            Debug.Log("File exists");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_fileName, FileMode.Open);

            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            _settings.CharacterPosition = new Vector3(save.SavedCharacterX, save.SavedCharacterY, save.SavedCharacterZ);
            _settings.DeadEnemyIds = save.SavedDeadEnemyIds;
            _settings.GainedMoneyIds = save.SavedGainedMoneyIds;
            _settings.OpenedChestIds = save.SavedOpenedChestIds;

            DeleteGainedBonuses();

            Debug.Log("Game data loaded!");
        }
        else
            Debug.Log("There is no save data!");

        return _settings;
    }

    public void ResetGame()
    {
        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);

            DeadEnemyIdsToSave.Clear();
            GainedMoneyIdsToSave.Clear();
            OpenedChestIdsToSave.Clear();
            _settings = new GameSettings();

            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
}