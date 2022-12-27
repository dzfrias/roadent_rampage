using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private List<Wheel> wheels;

    [Header("Specs")]
    public float speed;
    public float maxSpeed;
    [SerializeField] private float wheelBase;
    [SerializeField] private float rearTrack;
    [Tooltip("Higher turnRadius means less sharp turns")]
    [SerializeField] private float turnRadius;
    [SerializeField] private Vector3 centerOfMass;

    [Header("Mechanics")]
    [SerializeField] private float airSteer;

    // Inputs
    private float rotationInput;
    private float accelerationInput;

    [Header("Camera")]
    [SerializeField] private new GameObject camera;
    private CinemachineTilt cameraTilt;
    private CinemachinePushBack cameraPush;

    // Ground logic
    private bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        cameraTilt = camera.GetComponent<CinemachineTilt>();
        cameraPush = camera.GetComponent<CinemachinePushBack>();
    }

    void Update()
    {
        onGround = false;
        foreach (Wheel wheel in wheels)
        {
            if (wheel.onGround)
            {
                onGround = true;
                break;
            }
        }
        TiltCamera();
        if (onGround)
        {
            Steer();
        }
        if (rb.velocity.magnitude > maxSpeed - 1)
        {
            cameraPush.Activate();
        }
        else
        {
            cameraPush.Deactivate();
        }
    }

    void FixedUpdate()
    {
        if (!onGround)
        {
            AirSteer();
        }
        else
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    void TiltCamera()
    {
        if (onGround)
        {
            cameraTilt.SetTilt((CinemachineTiltDirection)accelerationInput);
        } else
        {
            cameraTilt.SetTilt(CinemachineTiltDirection.Rest);
        }
    }

    void AirSteer()
    {
        rb.AddTorque(transform.right * accelerationInput * airSteer, ForceMode.VelocityChange);
        rb.AddTorque(transform.up * rotationInput * airSteer, ForceMode.VelocityChange);
    }

    void Steer()
    {
        float angleLeft;
        float angleRight;
        if (rotationInput > 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * rotationInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * rotationInput;
        }
        else if (rotationInput < 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * rotationInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * rotationInput;
        }
        else
        {
            angleLeft  = 0;
            angleRight = 0;
        }

        foreach (Wheel wheel in wheels)
        {
            if (wheel.corner == Corner.FrontLeft)
            {
                wheel.steerAngle = angleLeft;
            }
            if (wheel.corner == Corner.FrontRight)
            {
                wheel.steerAngle = angleRight;
            }
        }
    }

    public void Turn(float value)
    {
        rotationInput = value;
    }

    public void Accelerate(float value)
    {
        accelerationInput = value;
    }
}
