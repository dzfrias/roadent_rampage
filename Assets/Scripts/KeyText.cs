using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    [SerializeField] private string prompt;
    [SerializeField] private float removeAfter = 2.5f;

    public void Activate()
    {
        GameManager.instance.BroadcastText(prompt);
        Time.timeScale = 0.5f;
        GameManager.instance.Darken();
        StartCoroutine(Remove());
    }

    public void Deactivate()
    {
        GameManager.instance.ClearBroadcast();
        GameManager.instance.Undarken();
        Time.timeScale = 1f;
    }

    IEnumerator Remove()
    {
        yield return new WaitForSecondsRealtime(removeAfter);
        Deactivate();
    }
}
