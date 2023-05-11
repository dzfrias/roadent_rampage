using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    private List<Button> contextDisabled;

    void Awake()
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

        contextDisabled = new List<Button>();
    }

    public void ContextDisable(Button button)
    {
        contextDisabled.Add(button);
    }

    public void ActivateContextDisable(bool enable)
    {
        foreach (var button in contextDisabled)
        {
            button.interactable = enable;
        }
    }
}
