using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContextActivate : MonoBehaviour
{
    void OnEnable()
    {
        Activate(false);
    }

    void OnDisable()
    {
        Activate(true);
    }

    public void Activate(bool enable)
    {
        MainMenuManager.instance.ActivateContextDisable(enable);
    }
}
