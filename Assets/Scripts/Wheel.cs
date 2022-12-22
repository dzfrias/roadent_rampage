using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Corner
{
    FrontLeft,
    FrontRight,
    BackLeft,
    BackRight,
}

public class Wheel : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Suspension")]
    [SerializeField] private float restLength;
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;

    [Header("Wheel")]
    public Corner corner;
    [SerializeField] private float radius;

    [SerializeField] private float steerTime;
    [System.NonSerialized] public float steerAngle;
    private float wheelAngle;

    [System.NonSerialized] public float accelerationInput;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, -steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);
    }

    void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, radius))
        {
            Vector3 springDir = transform.up;
            Vector3 tireVel = rb.GetPointVelocity(transform.position);
            float offset = restLength - hit.distance;
            float vel = Vector3.Dot(springDir, tireVel);
            float force = (offset * springStiffness) - (vel * damperStiffness);

            Vector3 wheelVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            float fx = accelerationInput * force;
            float fy = wheelVelocity.x  * force;

            rb.AddForceAtPosition((springDir * force) + (fx * transform.forward) + (fy * -transform.right), transform.position);
        }
    }
}
