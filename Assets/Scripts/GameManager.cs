using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GameManager : TapToFun.Singleton<GameManager>
{
    private static string NameSaveFile = "data";
    public enum TypeLevel {Defolt, Boiler }

    [SerializeField] private TypeLevel typeLevel;
    [SerializeField] private DataInstaller dataInstaller;
    [SerializeField] private Level level;
    [NonSerialized]  public static int levelNumber;

    private static int sessionNumber;
    [SerializeField] private SaveData saveData;
    private bool endGame;
    private bool play;

    public static DataInstaller DataInstaller => Instance.dataInstaller;
    public static Level Level => Instance.level;
    public static SaveData SaveData => Instance.saveData;
    

    public static bool EndGame => Instance.endGame;
    public static bool SetEndGame(bool value) => Instance.endGame = value;
    public static bool Play => Instance.play;

    public void Start()
    {
        StartLevel();
    }

    public static void SetLevelType(TypeLevel type)
    {
        Instance.typeLevel = type;
    }

    public void StartLevel()
    {
        levelNumber = saveData.level;
        if (levelNumber >= 6)
        {
            saveData.level = 0;
            levelNumber = saveData.level;
        }

        EventManager.OnLevel?.Invoke(levelNumber);
        level = Instantiate(dataInstaller.GetLevel(levelNumber), new Vector3(-114.706f,-96.119f,15.11899f), Quaternion.identity).GetComponent<Level>();
    }
    

    public static void StartGame()
    {    
        Instance.play = true;
        var parameters = new Dictionary<string, object>();
        parameters["Level"] = levelNumber + 1;
        Debug.Log("Log level number: "+ levelNumber);
    }

    public static void SaveOnWin()
    {
        levelNumber++;
        Save(true);
    } 
    
    public static void Save(bool win)
    {
        if (win)
            SaveData.level = levelNumber;
        SaveData.sessionNumber = sessionNumber;
        string json = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(NameSaveFile, json);
    }
    public static void Save()
    {
        string json = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(NameSaveFile, json);
    }

    public static SaveData Load()
    {
        SaveData saveData;
        if(PlayerPrefs.HasKey("data"))
        {
            saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(NameSaveFile));
        }else
        {
            levelNumber = 0;    
            saveData = new SaveData();
            saveData.level = levelNumber;
         
        }

        return saveData;
    }
    
}
