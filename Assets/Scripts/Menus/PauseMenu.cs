using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;

    void ChildrenActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    public void Resume()
    {
        AudioManager.instance.ResumeSounds();
        ChildrenActive(false);
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
            ChildrenActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
