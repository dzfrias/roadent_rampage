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
        lineRenderer.SetPosition(0, Vector3.zero);
        if (Physics.Raycast(Vector3.zero, transform.forward, out RaycastHit hit))
        {
            lineRenderer.SetPosition(1, transform.forward * hit.distance);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.forward * defaultDistance);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * defaultDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
