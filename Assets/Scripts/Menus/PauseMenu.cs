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
        GameManager.instance.Unpause();
        AudioManager.instance.ResumeSounds();
        ChildrenActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        GameManager.instance.Finish();
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
            GameManager.instance.Pause();
            ChildrenActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
