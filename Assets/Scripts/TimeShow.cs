using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TimeShow : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject time;
    private TMP_Text timerText;

    void Start()
    {
        timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        timerText.text = time.ToString();
    }
}
