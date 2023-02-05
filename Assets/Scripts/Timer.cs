using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    private TMP_Text timerText;
    private int minutes;
    private float seconds;
    private bool counting = true;

    void Start()
    {
        GameManager.onFinishLineReached += Stop;
        timerText = GetComponent<TMP_Text>();
    }

    void OnDestroy()
    {
        GameManager.onFinishLineReached -= Stop;
    }

    void Update()
    {
        if (!counting) { return; }
        seconds += Time.deltaTime;
        if (seconds >= 60f)
        {
            minutes += 1;
            seconds = 0f;
        }
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00.00");
    }

    public void Stop()
    {
        counting = false;
    }
}
