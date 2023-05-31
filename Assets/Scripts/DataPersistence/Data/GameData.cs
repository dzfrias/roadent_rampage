using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highestUnlockedLevel;
    public List<int> completedLevels;

    public GameData()
    {
        this.highestUnlockedLevel = 0;
        this.completedLevels = new();
    }
}
