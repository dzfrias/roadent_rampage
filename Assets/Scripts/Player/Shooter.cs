using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private bool generateImpulse;
    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        if (!generateImpulse)
        {
            return;
        }
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource is null)
        {
            Debug.LogError("generateImpulse set to `true` but no impulse source detected");
            enabled = false;
        }
    }

    public void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}!");
            if (hit.collider.gameObject.TryGetComponent(out IHittable hittableObject))
            {
                hittableObject.Hit(hit.point, 10);
            }
        }
        else
        {
            Debug.Log("Hit nothing");
        }
        impulseSource?.GenerateImpulse();
    }
}
