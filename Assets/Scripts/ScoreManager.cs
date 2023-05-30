using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject timeScriptableObject;
    [SerializeField] private TMP_InputField nameInput;
    private string time;
    private int score;

    public UnityEvent<string, int, string> submitScoreEvent;

    private void Start()
    {   
        time = timeScriptableObject.ToString();
        score = ConvertTimeToInt();
    }

    public void SubmitScore()
    {
        submitScoreEvent.Invoke(nameInput.text, score, timeScriptableObject.ToString());
    }

    private int ConvertTimeToInt()
    {
        return ((timeScriptableObject.minutes * 10000) + (int) Mathf.Round((timeScriptableObject.seconds * 100)));
    }
}
