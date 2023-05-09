using System.Text;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextBroadcast : MonoBehaviour
{
    private TMP_Text text;

    public static string InterpolatePrompt(string prompt)
    {
        PlayerInput input = PlayerInput.all[0];
        StringBuilder sb = new StringBuilder(prompt);
        if (input.currentControlScheme == "TwoGamepads")
        {
            Gamepad gamepad = Gamepad.current;
            string scheme;
            if (gamepad is UnityEngine.InputSystem.XInput.XInputController)
            {
                scheme = "Xbox Prompts";
            }
            else if (gamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
            {
                scheme = "PS5 Prompts";
            }
            else
            {
                scheme = "?";
            }
            
            sb.Replace("\\Forward", $"<sprite=\"{scheme}\" name=\"Right Trigger\">");
            sb.Replace("\\Turn", $"<sprite=\"{scheme}\" name=\"Left Stick\">");
            sb.Replace("\\Shoot", $"<sprite=\"{scheme}\" name=\"West\">");
            sb.Replace("\\Flip", $"<sprite=\"{scheme}\" name=\"South\">");
            sb.Replace("\\Aim", $"<sprite=\"{scheme}\" name=\"Right Stick\">");
        }
        else
        {
            sb.Replace("\\Forward", "<sprite=\"KBM Prompts\" name=\"W\">");
            sb.Replace("\\Shoot", "<sprite=\"KBM Prompts\" name=\"Space\">");
            sb.Replace("\\Flip", "<sprite=\"KBM Prompts\" name=\"F\">");
            sb.Replace("\\Aim", "mouse");
            sb.Replace("\\Turn", "<sprite=\"KBM Prompts\" name=\"Left\"><sprite=\"KBM Prompts\" name=\"Right\">");
        }

        return sb.ToString();
    }

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
        string t = InterpolatePrompt(newText);
        text.text = t;
    }

    void ClearText()
    {
        text.text = "";
    }
}
