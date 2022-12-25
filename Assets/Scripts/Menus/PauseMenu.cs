using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void MainMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(0);
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
