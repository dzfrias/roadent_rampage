using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    private bool paused;

    public void Resume()
    {
        ui.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    void OnPause()
    {
        if (paused)
        {
            Resume();
        }
        else
        {
            ui.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
