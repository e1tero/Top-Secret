using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DataInstaller", menuName = "Data Installer")]
public class DataInstaller : ScriptableObject
{
    [Header("Levels")]
    [SerializeField] private bool testLevel;
    [SerializeField] private int patternLevel = 0;
    [SerializeField] private List<GameObject> levels;
    
    public bool isRestart;
    public GameObject GetLevel(int index)
    {
        if (testLevel)
            index = patternLevel;
        else
            index = GameManager.levelNumber;
        
        if (isRestart)
        {
            isRestart = false;
            return levels[GameManager.SaveData.currentLevel];
        }

        //GameManager.SaveData.currentLevel = index;
        return levels[index];
    }
}



