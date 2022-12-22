using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Player Finished The Level!");
        }
    }
}
