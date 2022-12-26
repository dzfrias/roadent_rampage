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
    [SerializeField] private float wheelBase;
    [SerializeField] private float rearTrack;
    [Tooltip("Higher turnRadius means less sharp turns")]
    [SerializeField] private float turnRadius;
    [SerializeField] private Vector3 centerOfMass;

    [Header("Mechanics")]
    [Tooltip("Time needed to be flipped for player to be able to unflip")]
    [SerializeField] private float unflipTime;
    [SerializeField] float airSteer;
    [SerializeField] float airSteerMax;

    // Inputs
    private float rotationInput;
    private float accelerationInput;

    // Flipped logic
    private bool flipped;
    private float flippedTimer;

    // Camera
    private CameraController cameraController;
    private CinemachineTilt cameraTilt;

    // Ground logic
    private bool onGround;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        GameObject cam = GameObject.FindWithTag("Camera");
        cameraController = cam.GetComponent<CameraController>();
        cameraTilt = cam.GetComponent<CinemachineTilt>();
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

        if (flipped)
        {
            flippedTimer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!onGround)
        {
            AirSteer();
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
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, airSteerMax);
    }

    void Steer()
    {
        float angleLeft;
        float angleRight;
        if (rotationInput > 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * rotationInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * rotationInput;
        } else if (rotationInput < 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * rotationInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * rotationInput;
        } else
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
            wheel.accelerationInput = accelerationInput;
        }
    }

    void OnCameraChange(InputValue _value)
    {
        cameraController.ChangeTarget();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Collision with ground
        if (collision.gameObject.layer == 3)
        {
            flipped = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Collision with ground
        if (collision.gameObject.layer == 3)
        {
            flipped = false;
            flippedTimer = 0f;
        }
    }

    void OnFlip(InputValue _value)
    {
        if (flippedTimer > unflipTime)
        {
            transform.rotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.forward);
        }
    }

    void OnTurn(InputValue value)
    {
        rotationInput = value.Get<float>();
    }

    void OnAccelerate(InputValue value)
    {
        accelerationInput = value.Get<float>();
    }
}
