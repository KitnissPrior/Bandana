using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public CharacterSpawner CharacterSpawner;
    public float CharacterXToSave;
    public float CharacterYToSave;
    public float CharacterZToSave;
    public List<int> DeadEnemyIdsToSave;
    public List<int> OpenedChestIdsToSave;
    public List<int> GainedMoneyIdsToSave;
    public List<int> CrystalsIdsToSave;
    public CurrentBonuses CurrentBonuses;
    public CommonData CommonData;

    private Vector3 _characterPosition;
    private Character _character;
    private GameSettings _settings;

    void Start()
    {
        _settings = new GameSettings();
        _settings.Initialize();
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
        CharacterYToSave = _character.transform.position.y;
        CharacterZToSave = _character.transform.position.z;
    }

    public void DeleteGainedBonuses()
    {
        if (_settings.OpenedChestIds != null)
            foreach (var id in _settings.OpenedChestIds)
                Destroy(CurrentBonuses.Chests[id].gameObject);
        if (_settings.GainedMoneyIds != null)
            foreach (var id in _settings.GainedMoneyIds)
                Destroy(CurrentBonuses.Coins[id].gameObject);
        if (_settings.CrystalsIds != null) 
            foreach (var id in _settings.CrystalsIds)
                Destroy(CurrentBonuses.Crystals[id].gameObject);
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

    public List<int> GetCrystals()
    {
        CrystalsIdsToSave = CurrentBonuses.CheckCrystals();
        Debug.Log(CrystalsIdsToSave.Count);
        return CrystalsIdsToSave;
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

    public void AddCrystal(int id)
    {
        if (!_settings.CrystalsIds.Contains(id))
            _settings.CrystalsIds.Add(id);
    }

    private SaveData CreateSaveGameObject()
    {
        SaveData save = new SaveData();
        GetPositions();
        CommonData.NextScene = SceneManager.GetActiveScene().name;

        save.SavedCharacterX = CharacterXToSave;
        save.SavedCharacterY = CharacterYToSave;
        save.SavedCharacterZ = CharacterZToSave;
        save.SavedDeadEnemyIds = GetDeadEnemies();
        save.SavedGainedMoneyIds = GetGainedMoney();
        save.SavedOpenedChestIds = GetOpenedChests();
        if (CommonData.CurrentLevel == 2)
            save.SavedCrystalsIds = GetCrystals();

        return save;
    }

    public void SaveGame()
    {
        SaveData save = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(CommonData.SavedDataFile);

        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game data saved");

    }

    public GameSettings LoadGame(bool shouldReset)
    {
        if (shouldReset)
        {
            ResetGame();
            Debug.Log("There is no save data!");
        }
        else if (CommonData.IsSavedData())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(CommonData.SavedDataFile, FileMode.Open);

            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            Start();
            _settings.CharacterPosition = new Vector3(save.SavedCharacterX, save.SavedCharacterY, save.SavedCharacterZ);
            _settings.DeadEnemyIds = save.SavedDeadEnemyIds;
            _settings.GainedMoneyIds = save.SavedGainedMoneyIds;
            _settings.OpenedChestIds = save.SavedOpenedChestIds;
            _settings.CrystalsIds = save.SavedCrystalsIds;

            DeleteGainedBonuses();

            Debug.Log("Game data loaded!");
        }
        else
        {
            _settings = new GameSettings();
            _settings.Initialize();
            CommonData.SetCrystalsCount(-CommonData.CrystalsCount);
        }

        return _settings;
    }

    public void ResetGame()
    {
        if (CommonData.IsSavedData())
        {
            File.Delete(CommonData.SavedDataFile);

            DeadEnemyIdsToSave.Clear();
            GainedMoneyIdsToSave.Clear();
            OpenedChestIdsToSave.Clear();
            CrystalsIdsToSave.Clear();
            CommonData.SetCrystalsCount(-CommonData.CrystalsCount);
            CommonData.NotResetGame();

            Debug.Log("Data reset complete!");
        }
        else
            Debug.Log("No save data to delete.");

        _settings = new GameSettings();
        _settings.Initialize();
    }
}