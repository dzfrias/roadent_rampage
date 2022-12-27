using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboScorer : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> comboFinished;
    [SerializeField] private float airTimeMultiplier;
    [SerializeField] private float scoreThreshold = 10f;

    private bool onGround;
    private float runningScore;

    void Update()
    {
        if (!onGround)
        {
            runningScore += Time.deltaTime * airTimeMultiplier;
        }
    }

    public void LeftGround()
    {
        onGround = false;
    }

    public void HitGround()
    {
        onGround = true;
        if (runningScore > scoreThreshold)
        {
            comboFinished.Invoke(runningScore);
            Debug.Log($"Finished combo with {runningScore}!");
            runningScore = 0f;
        }
    }
}
