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

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameManager.instance.Unpause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TogglePause()
    {
        if (paused)
        {
            GameManager.instance.Unpause();
        }
        else
        {
            GameManager.instance.Pause();
        }
        paused = !paused;
        ChildrenActive(paused);
    }
}
