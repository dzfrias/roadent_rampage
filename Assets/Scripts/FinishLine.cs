using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private Timer timer;
    
    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    public void FinishLevel() 
    {
        Debug.Log("Player Finished The Level!");
        timer?.Stop();
    }
}
