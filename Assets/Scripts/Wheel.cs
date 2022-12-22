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
    private PlayerMovement car;
    private Rigidbody carRb;

    [Header("Suspension")]
    [SerializeField] private float restLength;
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;

    [Header("Wheel")]
    public Corner corner;
    [SerializeField] private float radius;
    [SerializeField] private float grip;
    [SerializeField] private float mass;

    [SerializeField] private float steerTime;
    [System.NonSerialized] public float steerAngle;
    private float wheelAngle;

    [System.NonSerialized] public float accelerationInput;

    void Start()
    {
        car = transform.root.GetComponent<PlayerMovement>();
        carRb = transform.root.GetComponent<Rigidbody>();
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
            // TODO: Proper acceleration force
            carRb.AddForceAtPosition(transform.forward * accelerationInput * car.speed, transform.position);

            ApplySpring(hit);
            ApplySlip();
        }
    }

    void ApplySpring(RaycastHit hit)
    {
        Vector3 springDir = transform.up;
        Vector3 tireVel = carRb.GetPointVelocity(transform.position);
        float offset = restLength - hit.distance;
        float vel = Vector3.Dot(springDir, tireVel);
        float force = (offset * springStiffness) - (vel * damperStiffness);

        /* Vector3 wheelVelocity = transform.InverseTransformDirection(carRb.GetPointVelocity(hit.point)); */
        /* float fx = accelerationInput * force; */
        /* float fy = wheelVelocity.x  * force; */

        carRb.AddForceAtPosition(springDir * force, transform.position);
    }

    void ApplySlip()
    {
        Vector3 steeringDir = transform.right;
        Vector3 tireVel = carRb.GetPointVelocity(transform.position);
        float steerVel = Vector3.Dot(steeringDir, tireVel);
        float velChange = -steerVel * grip;
        float accelChange = velChange / Time.fixedDeltaTime;
        carRb.AddForceAtPosition(steeringDir * mass * accelChange, transform.position);
    }
}
