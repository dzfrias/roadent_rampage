using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private string prompt;

    private bool active;

    public void Activate()
    {
        active = true;
        GameManager.instance.BroadcastText(prompt);
        Time.timeScale = 0.5f;
        GameManager.instance.Darken();
    }

    void Update()
    {
        if (!active) return;

        if (Input.GetKeyDown(key))
        {
            active = false;
            GameManager.instance.ClearBroadcast();
            GameManager.instance.Undarken();
            Time.timeScale = 1f;
        }
    }
}
