using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Wheel> wheels;

    [Header("Specs")]
    public float speed;
    [SerializeField] private float wheelBase;
    [SerializeField] private float rearTrack;
    [Tooltip("Higher turnRadius means less sharp turns")]
    [SerializeField]
    private float turnRadius;
    [SerializeField] private Vector3 centerOfMass;

    [Header("Mechanics")]
    [Tooltip("Time needed to be flipped for player to be able to unflip")]
    [SerializeField] private float unflipTime;

    // Inputs
    private float rotationInput;
    private float accelerationInput;

    // Flipped logic
    private bool flipped;
    private float flippedTimer;

    // Camera
    private CameraController cameraController;
    private CinemachineTilt cameraTilt;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        GameObject cam = GameObject.FindWithTag("Camera");
        cameraController = cam.GetComponent<CameraController>();
        cameraTilt = cam.GetComponent<CinemachineTilt>();
    }

    void Update()
    {
        TiltCamera();
        Steer();

        if (flipped)
        {
            flippedTimer += Time.deltaTime;
        }
    }

    void TiltCamera()
    {
        bool onGround = false;
        foreach (Wheel wheel in wheels)
        {
            if (wheel.onGround)
            {
                onGround = true;
                break;
            }
        }
        if (onGround)
        {
            cameraTilt.SetTilt((CinemachineTiltDirection)accelerationInput);
        } else
        {
            cameraTilt.SetTilt(CinemachineTiltDirection.Rest);
        }
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
