using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float rotateAmount;
    [SerializeField] private List<Transform> wheels;

    [Header("Spring")]
    [SerializeField] private float suspensionRest;
    [SerializeField] private float springStrength;
    [SerializeField] private float springDamper;

    [Header("Grip")]
    [SerializeField] private float wheelGrip;
    [SerializeField] private float wheelMass;

    [Header("Speed")]
    [SerializeField] private float topSpeed;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckRadius = 0.4f;
    private LayerMask groundMask;
    private Transform groundCheck;

    private Rigidbody rb;

    // Inputs
    private float accelerationInput;
    private float rotationInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationInput * rotateAmount, 0));
        foreach (Transform wheel in wheels)
        {
            RaycastHit hit;
            if (Physics.Raycast(wheel.position, Vector3.down, out hit, 1f))
            {
                ApplySuspension(wheel, hit);
                ApplySteering(wheel);
                ApplyAcceleration(wheel);
            }
        }
    }

    void OnAccelerate(InputValue value)
    {
        accelerationInput = value.Get<float>();
    }

    void OnTurn(InputValue value)
    {
        rotationInput = value.Get<float>();
    }

    void ApplySuspension(Transform wheel, RaycastHit hit)
    {
        Vector3 springDir = wheel.up;
        Vector3 tireVel = rb.GetPointVelocity(wheel.position);
        float offset = suspensionRest - hit.distance;
        float vel = Vector3.Dot(springDir, tireVel);
        float force = (offset * springStrength) - (vel * springDamper);
        rb.AddForceAtPosition(springDir * force, wheel.position);
    }

    void ApplySteering(Transform wheel)
    {
        Vector3 steeringDir = wheel.right;
        Vector3 tireVel = rb.GetPointVelocity(wheel.position);
        float steeringVel = Vector3.Dot(steeringDir, tireVel);
        float velChange = -steeringVel * wheelGrip;
        float accelChange = velChange / Time.fixedDeltaTime;
        rb.AddForceAtPosition(steeringDir * wheelMass * accelChange, wheel.position);
    }

    void ApplyAcceleration(Transform wheel)
    {
        Vector3 accelDir = wheel.forward;
        if (accelerationInput > 0f)
        {
            float carSpeed = Vector3.Dot(transform.forward, rb.velocity);
            float normalSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / topSpeed);
            float torque = 10f;
            rb.AddForceAtPosition(accelDir * torque, wheel.position);
        }
    }
}
