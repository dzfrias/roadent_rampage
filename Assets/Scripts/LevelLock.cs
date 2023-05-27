using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    [SerializeField] private GameObject levelSelect;
    private List<Button> levelButtons = new();
    private int highestUnlockedLevel = 0;

    void Start()
    {   
        highestUnlockedLevel = LevelManager.instance.GetHighestUnlockedLevel();

        foreach (Transform child in levelSelect.transform)
        {
            Button levelButton = child.GetComponent<Button>();
            levelButtons.Add(levelButton);
        }

        for (int i = 0; i <= highestUnlockedLevel; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
}
