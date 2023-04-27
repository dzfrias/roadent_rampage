using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ForwardLineDraw : MonoBehaviour
{
    [SerializeField] private float defaultDistance = 100f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        // Ignores IgnoreRaycast and IgnoreShoot layers
        LayerMask mask = ~0b10000100;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, mask))
        {
            lineRenderer.SetPosition(1, transform.forward * hit.distance + transform.position);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.forward * defaultDistance + transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * defaultDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
