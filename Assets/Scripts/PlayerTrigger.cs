using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    [System.NonSerialized] public bool isColliding;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("PlayerBody"))
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("PlayerBody"))
        {
            isColliding = false;
        }
    }
}
