using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent triggered;
    [SerializeField] private UnityEvent stopTriggered;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            triggered.Invoke();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            stopTriggered.Invoke();
        }
    }
}
