using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onObjectDestroy;

    void OnDestroy()
    {
        onObjectDestroy.Invoke();
    }
}
