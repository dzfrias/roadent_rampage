using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    public static LevelManager instance;
    private int highestUnlockedLevel = 0;
    private List<Scene> completedLevels = new();

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
        if (!completedLevels.Contains(SceneManager.GetActiveScene()))
        {
            highestUnlockedLevel++;
            completedLevels.Add(SceneManager.GetActiveScene());
            Debug.Log("INCREASED HIGHEST LEVEL" + highestUnlockedLevel);
        }
    }

    public void LoadData(GameData data)
    {
        this.highestUnlockedLevel = data.highestUnlockedLevel;
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
