using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Suspension")]
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;

    private float minLength;
    private float maxLength;
    private float springLength;

    [Header("Wheel")]
    [SerializeField] private float radius;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + radius))
        {
            float lastLength = springLength;
            springLength = Mathf.Clamp(hit.distance - radius, minLength, maxLength);
            float springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            float springForce = springStiffness * (restLength - springLength);
            float damperForce = damperStiffness * springVelocity;

            Vector3 suspensionForce = (springForce + damperForce) * transform.up;

            rb.AddForceAtPosition(suspensionForce, hit.point);
        }
    }
}
