using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ContinuousImpulse : MonoBehaviour
{
    [SerializeField] private float interval = 0.1f;

    private CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        while (true)
        {
            impulseSource.GenerateImpulse();
            yield return new WaitForSeconds(interval);
        }
    }
}
