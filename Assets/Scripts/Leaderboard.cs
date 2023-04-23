using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject time;
    [SerializeField] private TMP_Text text;

    IEnumerator Start()
    {
        yield return SubmitScore();
        yield return GetScores();
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

        yield return new WaitWhile(() => !done);
    }

    IEnumerator GetScores()
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        int index = 1;
        LootLockerSDKManager.GetScoreList("highscore", 10, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully retrieved score");

                StringBuilder sb = new StringBuilder();
                foreach (var score in response.items)
                {
                    float realScore = (float)score.score / 100;
                    int minutes = (int)(realScore / 60);
                    float seconds = realScore % 60;
                    sb.AppendFormat("{0}. {1}m {2}s", index, minutes, seconds);
                    index++;
                }

                text.text = sb.ToString();

                done = true;
            }
            else
            {
                Debug.Log("Problem retrieving score: " + response.Error);
                text.text = "Error getting score: " + response.Error;
                done = true;
            }
        });

        yield return new WaitWhile(() => !done);
    }
}
