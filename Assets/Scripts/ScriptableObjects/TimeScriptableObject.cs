using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TimeScriptableObject")]
public class TimeScriptableObject : ScriptableObject
{
    public int minutes;
    public float seconds;
    public int levelIndex;

    public override string ToString()
    {
        return minutes.ToString("00") + ":" + seconds.ToString("00.00");
    }
}
