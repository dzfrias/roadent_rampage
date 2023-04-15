using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject finalTime;

    private int minutes;
    private float seconds;
    private bool counting = true;

    void Start()
    {
        GameManager.onFinishLineReached += Stop;
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
        finalTime.minutes = minutes;
        finalTime.seconds = seconds;
    }

    public void Stop()
    {
        counting = false;
    }
}
