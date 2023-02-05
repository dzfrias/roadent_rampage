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
        AudioManager.instance.ResumeSounds();
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
            AudioManager.instance.PauseSounds();
            ui.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
