using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    private bool paused;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void Resume()
    {
        canvas.enabled = false;
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
            canvas.enabled = true;
            Time.timeScale = 0f;
            paused = true;
        }
    }
}
