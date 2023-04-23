using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject time;

    private const int leaderboardID = 13656;

    void Start()
    {
        StartCoroutine(SubmitScore());
    }

    IEnumerator SubmitScore()
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        float score = time.minutes * 60 + time.seconds;
        int scoreInt = (int)(score * 100);
        LootLockerSDKManager.SubmitScore(playerID, scoreInt, "highscore", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
