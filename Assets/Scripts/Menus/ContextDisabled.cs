using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContextDisabled : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        MainMenuManager.instance.ContextDisable(btn);
    }
}
