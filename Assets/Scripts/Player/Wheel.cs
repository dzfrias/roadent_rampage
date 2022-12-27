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
    private PlayerController car;
    private Rigidbody carRb;

    [Header("Suspension")]

    [Tooltip("Higher restLength means higher above the ground")]
    [SerializeField]
    private float restLength;

    [Tooltip("Higher springStiffness means more dramatic springs")]
    [SerializeField]
    private float springStiffness;

    [Tooltip("Higher damperStiffness means less springiness")]
    [SerializeField]
    private float damperStiffness;

    [SerializeField] private float radius;

    [Header("Wheel")]
    public Corner corner;
    [Range(0, 1)]
    [Tooltip("Higher grip means less sideways slip.")]
    [SerializeField] private float grip;
    [SerializeField] private float mass;
    [SerializeField] private float forwardsGrip;

    [Tooltip("Higher steerTime means a slower time to turn the wheels of the car")]
    [SerializeField] private float steerTime;
    [HideInInspector] public float steerAngle;
    private float wheelAngle;

    private float accelerationInput;
    [HideInInspector] public bool onGround { get; private set; }

    void Start()
    {
        car = transform.root.GetComponent<PlayerController>();
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
            onGround = true;
            if (carRb.GetPointVelocity(transform.position).magnitude <= car.maxSpeed)
            {
                carRb.AddForceAtPosition(transform.forward * car.speed * accelerationInput, transform.position);
            }
            if (accelerationInput == 0) 
            {
                carRb.AddForceAtPosition(-carRb.GetPointVelocity(transform.position) * forwardsGrip, transform.position);
            }
            ApplySpring(hit);
            ApplySlip();
        }
        else
        {
            onGround = false;
        }
    }

    void ApplySpring(RaycastHit hit)
    {
        Vector3 springDir = transform.up;
        Vector3 tireVel = carRb.GetPointVelocity(transform.position);
        float offset = restLength - hit.distance;
        float vel = Vector3.Dot(springDir, tireVel);
        float force = (offset * springStiffness) - (vel * damperStiffness);

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

    public void Accelerate(float value)
    {
        accelerationInput = value;
    }
}
