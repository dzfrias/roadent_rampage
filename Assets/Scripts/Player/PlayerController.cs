using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [Header("Specs")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Vector3 centerOfMass;

    [Header("Mechanics")]
    [SerializeField] private GameObject moverObject;
    [SerializeField] private float airSteer;
    [SerializeField, Range(0, 1)] private float directionChangeFactor;

    [Header("Camera")]
    [SerializeField] private new GameObject camera;
    private CinemachineTilt cameraTilt;
    private CinemachinePushBack cameraPush;


    // Core
    private Rigidbody rb;
    private IMover mover;
    private ComboScorer comboScorer;

    // Ground logic
    private bool onGround;

    // Inputs
    private float rotationInput;
    private float accelerationInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        cameraTilt = camera.GetComponent<CinemachineTilt>();
        cameraPush = camera.GetComponent<CinemachinePushBack>();
        comboScorer = GetComponent<ComboScorer>();
        mover = moverObject.GetComponent<IMover>();
    }

    void Update()
    {
        bool previousOnGround = onGround;
        onGround = mover.IsGrounded();
        TiltCamera();
        if (onGround)
        {
            mover.Turn(rotationInput);
            mover.Accelerate(speed * accelerationInput);
        }
        if (previousOnGround != onGround && onGround)
        {
            comboScorer.HitGround();
        }
        else if (previousOnGround != onGround && !onGround)
        {
            comboScorer.LeftGround();
        }
        if (rb.velocity.magnitude > mover.MaxSpeed - 1)
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

    public void SpeedBoost(float amount)
    {
        if (accelerationInput == -1)
        {
            rb.AddForce(-transform.forward * amount * 2, ForceMode.VelocityChange);
        }
        else
        {
            rb.AddForce(transform.forward * amount, ForceMode.VelocityChange);
        }
    }

    public void Turn(float value)
    {
        rotationInput = value;
    }

    public void Accelerate(float value)
    {
        accelerationInput = value;
        if (onGround && value != 0 && Mathf.Sign(value) != Mathf.Sign(rb.velocity.z))
        {
            // Calling this in Update() is fine, as one time impulses are 
            // better suited in Update()
            rb.AddForce(transform.forward * value * (Mathf.Abs(rb.velocity.z) * directionChangeFactor), ForceMode.VelocityChange);
        }
    }
}
