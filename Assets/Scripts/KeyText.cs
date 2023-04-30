using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    [SerializeField] private List<string> keys;
    [SerializeField] private string prompt;
    [SerializeField] private float removeAfter = 2.5f;

    private bool active;

    public void Activate()
    {
        active = true;
        GameManager.instance.BroadcastText(prompt);
        Time.timeScale = 0.5f;
        GameManager.instance.Darken();
        StartCoroutine(Remove());
    }

    public void Deactivate()
    {
        active = false;
        GameManager.instance.ClearBroadcast();
        GameManager.instance.Undarken();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!active) return;

        if (keys.Any(key => Input.GetKeyDown(key)))
        {
            Deactivate();
        }
    }

    IEnumerator Remove()
    {
        yield return new WaitForSecondsRealtime(removeAfter);
        if (!active) yield break;
        Deactivate();
    }
}
