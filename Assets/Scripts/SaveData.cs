using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class SaveData: ScriptableObject
{
    public int level;
    public int coins;
    public int currentLevel;
    public int record;
    public int sessionNumber;
    
    
    public SaveData()
    {
        record = 0;
        coins = 0;
        level = 0;
        currentLevel = 0;
        sessionNumber = 0;
    }
}
