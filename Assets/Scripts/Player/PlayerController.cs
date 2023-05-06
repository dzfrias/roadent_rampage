using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody), typeof(IMover))]
public class PlayerController : MonoBehaviour
{
    [Header("Specs")]
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Vector3 centerOfMass;

    [Header("Mechanics")]
    [SerializeField] private float airSteer;
    [SerializeField, Range(0, 1)] private float directionChangeFactor;
    [SerializeField] private float skidThreshold = 20f;

    [Header("Camera")]
    [SerializeField] private new GameObject camera;

    // Core
    private Rigidbody rb;
    private IMover mover;

    // Camera
    private CinemachineTilt cameraTilt;
    private CinemachinePushBack cameraPush;

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
        mover = GetComponent<IMover>();

        AudioManager.instance.Play("accelerate");
    }

    void Update()
    {
        onGround = mover.IsGrounded();
        TiltCamera();
        if (onGround)
        {
            mover.Turn(rotationInput);
            mover.Accelerate(speed * accelerationInput);
            AdjustAcceleratePitch();
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
        if (Mathf.Abs(Vector3.Dot(transform.right, rb.velocity)) > skidThreshold)
        {
            // TODO: Add sound here
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

    void AdjustAcceleratePitch()
    {
        AudioManager.instance.SetPitch("accelerate", rb.velocity.magnitude / 25);
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

        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        if (onGround && value != 0 && Mathf.Sign(value) != Mathf.Sign(localVelocity.z))
        {
            // Calling this in Update() is fine, as one time impulses are 
            // better suited in Update()
            rb.AddForce(transform.forward * value * (Mathf.Abs(localVelocity.z) * directionChangeFactor), ForceMode.VelocityChange);
        }
    }

    public void Stun(float seconds)
    {
        StartCoroutine(StunForSeconds(seconds));
    }

    IEnumerator StunForSeconds(float seconds)
    {
        Vector3 startPos = transform.position;
        float s = seconds;
        while (s >= 0f)
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
            s -= Time.deltaTime;
            yield return null;
        }
    }
}
