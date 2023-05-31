using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int lastLevelIndex;
    public static LevelManager instance;
    private int highestUnlockedLevel = 0;
    private List<int> completedLevels = new();

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        GameManager.onFinishLineReached += UnlockLevel;
    }

    private void OnDestroy() 
    {
        GameManager.onFinishLineReached -= UnlockLevel;
    }

    void UnlockLevel()
    {
        if (!completedLevels.Contains(SceneManager.GetActiveScene().buildIndex) && SceneManager.GetActiveScene().buildIndex != lastLevelIndex)
        {
            highestUnlockedLevel++;
            completedLevels.Add(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadData(GameData data)
    {
        this.highestUnlockedLevel = data.highestUnlockedLevel;
        this.completedLevels = data.completedLevels;
    }

    public void SaveData(ref GameData data)
    {
        data.highestUnlockedLevel = this.highestUnlockedLevel;
    }

    public int GetHighestUnlockedLevel()
    {
        return highestUnlockedLevel;
    }
}
