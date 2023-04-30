using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    [SerializeField] private List<string> keys;
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

        if (keys.Any(key => Input.GetKeyDown(key)))
        {
            active = false;
            GameManager.instance.ClearBroadcast();
            GameManager.instance.Undarken();
            Time.timeScale = 1f;
        }
    }
}
