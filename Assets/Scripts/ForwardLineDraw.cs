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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            lineRenderer.SetPosition(1, transform.forward * hit.distance + transform.position);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.forward * defaultDistance + transform.position);
        }
    }
}
