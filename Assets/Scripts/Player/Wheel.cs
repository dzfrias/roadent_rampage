using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, IMover
{
    public float MaxSpeed { get; set; }
    [SerializeField] private Rigidbody target;

    [Header("Suspension")]
    [SerializeField, Tooltip("Higher restLength means higher above the ground")]
    private float restLength;
    [SerializeField, Tooltip("Higher springStiffness means more dramatic springs")]
    private float springStiffness;
    [SerializeField, Tooltip("Higher damperStiffness means less springiness")]
    private float damperStiffness;

    [Header("Wheel")]
    [SerializeField] private float radius;
    [Range(0, 1), SerializeField, Tooltip("Higher grip means less sideways slip.")]
    private float grip;
    [SerializeField] private float mass;
    [SerializeField] private float forwardsGrip;
    [SerializeField] private float terrainGripModifier;
    [Range(0, 90), SerializeField, Tooltip("Angle wheel can climb up to")] private float maxClimbAngle = 45f;
    [SerializeField, Tooltip("Higher steerTime means a slower time to turn the wheels of the car")]
    private float steerTime;

    // Private fields
    private float wheelAngle;
    private float acceleration;
    private float steerAngle;
    private bool onGround;

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
            if (Vector3.Angle(Vector3.down, -transform.up) > maxClimbAngle && transform.forward.y >= 0)
            {
                acceleration = 0;
            }
            if (target.GetPointVelocity(transform.position).magnitude <= MaxSpeed)
            {
                target.AddForceAtPosition(transform.forward * acceleration, transform.position);
            }
            if (acceleration == 0)
            {
                target.AddForceAtPosition(-target.GetPointVelocity(transform.position) * forwardsGrip, transform.position);
            }
            if (hit.collider.gameObject.CompareTag("Terrain")) 
            {
                target.AddForceAtPosition(-target.GetPointVelocity(transform.position) * terrainGripModifier, transform.position);
            }
            if (hit.collider.gameObject.TryGetComponent(out IDriveSurface driveSurface))
            {
                driveSurface.WheelHit(this);
            }
            ApplySpring(hit);
            ApplySlip();
        }
        else
        {
            onGround = false;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        target.AddForceAtPosition(force, transform.position);
    }

    public Vector3 Velocity()
    {
        return target.GetPointVelocity(transform.position);
    }

    void ApplySpring(RaycastHit hit)
    {
        Vector3 springDir = transform.up;
        Vector3 tireVel = target.GetPointVelocity(transform.position);
        float offset = restLength - hit.distance;
        float vel = Vector3.Dot(springDir, tireVel);
        float force = (offset * springStiffness) - (vel * damperStiffness);

        target.AddForceAtPosition(springDir * force, transform.position);
    }

    void ApplySlip()
    {
        Vector3 steeringDir = transform.right;
        Vector3 tireVel = target.GetPointVelocity(transform.position);
        float steerVel = Vector3.Dot(steeringDir, tireVel);
        float velChange = -steerVel * grip;
        float accelChange = velChange / Time.fixedDeltaTime;
        target.AddForceAtPosition(steeringDir * mass * accelChange, transform.position);
    }

    public void Accelerate(float value)
    {
        acceleration = value;
    }

    public void Turn(float angle)
    {
        steerAngle = angle;
    }

    public bool IsGrounded()
    {
        return onGround;
    }
}
