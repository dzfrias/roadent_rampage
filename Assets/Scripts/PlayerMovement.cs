using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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

    // Other
    private bool flipped;
    private float flippedTimer;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    void Update()
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

        if (flipped)
        {
            flippedTimer += Time.deltaTime;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // Collision with ground
        if (collision.gameObject.layer == 3)
        {
            flipped = true;
        }
    }

    void OnFlip(InputValue _value)
    {
        if (flippedTimer > unflipTime)
        {
            transform.rotation = Quaternion.identity;
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

    void OnTurn(InputValue value)
    {
        rotationInput = value.Get<float>();
    }

    void OnAccelerate(InputValue value)
    {
        accelerationInput = value.Get<float>();
    }
}
