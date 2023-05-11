using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyText : MonoBehaviour
{
    [SerializeField] private string prompt;
    [SerializeField] private float removeAfter = 2.5f;
    [SerializeField] private float timeSlowDown = 0.5f;

    public void Activate()
    {
        GameManager.instance.BroadcastText(prompt);
        Time.timeScale = timeSlowDown;
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
        yield return new WaitForSeconds(removeAfter / (1 / timeSlowDown));
        Deactivate();
    }
}
