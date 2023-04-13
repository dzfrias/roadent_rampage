using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoad : MonoBehaviour
{
    void Start()
    {
        // In the case of coming in from one of the levels, always default to
        // unlocking the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
