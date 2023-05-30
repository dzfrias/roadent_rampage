using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TimeScriptableObject timeScriptableObject;
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string[] levelPublicLeaderboardKeys = { 
        "eb00f360308cba93bda8030282bc0ef1108eb0c33716dc4db229ab4fadf266e2",
        "c5bdb74d73914d0e1dab95b0273db96b1ca7ef1cafdf628ca27b0c95f389ba1c",
        "07d7f5132b445069050aee64096a839a9c2ac755d32e887a5b744985d32b09ff",
        "926cdb77945ff09c68b57a52ccc6003d6813058da22b8f6e0d6fd9588c2abb98"
    };

    private string currentLevelPublicLeaderboardKey;

    private void Start()
    {
        currentLevelPublicLeaderboardKey = levelPublicLeaderboardKeys[timeScriptableObject.levelIndex];
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(currentLevelPublicLeaderboardKey, ((message) => {
            int loopLength = (message.Length < names.Count) ? message.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                int reverseOrderIndex = loopLength - 1 - i;
                names[i].text = message[reverseOrderIndex].Username;
                scores[i].text = message[reverseOrderIndex].Extra;
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score, string time)
    {
        LeaderboardCreator.UploadNewEntry(currentLevelPublicLeaderboardKey, username, score, time, ((message) => {
            GetLeaderboard();
        }));
    }
}
