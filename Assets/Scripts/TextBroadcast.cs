using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextBroadcast : MonoBehaviour
{
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.onBroadcastText += ShowText;
        GameManager.onClearBroadcast += ClearText;
    }

    void OnDestroy()
    {
        GameManager.onBroadcastText -= ShowText;
        GameManager.onClearBroadcast -= ClearText;
    }

    void ShowText(string newText)
    {
        text.text = newText;
    }

    void ClearText()
    {
        text.text = "";
    }
}
