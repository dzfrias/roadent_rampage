using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public void FinishLevel() 
    {
        GameManager.instance.Finish();
    }
}
