using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    [SerializeField] private float deactivateAfter = 2f;

    void Start()
    {
        GameManager.instance.BroadcastText("Run.");
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSecondsRealtime(deactivateAfter);

        gameObject.SetActive(false);
        GameManager.instance.ClearBroadcast();
    }
}
