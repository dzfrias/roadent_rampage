using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCamera : MonoBehaviour
{
    [SerializeField] private float deactivateAfter = 2f;
    [SerializeField] private UnityEvent onComplete;

    void Start()
    {
        GameManager.instance.BroadcastText("Run.");
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(deactivateAfter);

        GameManager.instance.ClearBroadcast();
        onComplete?.Invoke();
    }
}
