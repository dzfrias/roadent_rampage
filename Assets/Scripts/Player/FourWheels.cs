using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWheels : MonoBehaviour, IMover
{
    [SerializeField] private float maxSpeed;
    public float MaxSpeed { get; set; }

    [Header("Wheels")]
    [SerializeField] private Wheel topRight;
    [SerializeField] private Wheel topLeft;
    [SerializeField] private Wheel bottomRight;
    [SerializeField] private Wheel bottomLeft;

    [Header("Specs")]
    [SerializeField] private float turnRadius;
    [SerializeField] private float rearTrack;
    [SerializeField] private float wheelBase;

    void Start()
    {
        MaxSpeed = maxSpeed;
        topRight.MaxSpeed = MaxSpeed;
        topLeft.MaxSpeed = MaxSpeed;
        bottomRight.MaxSpeed = MaxSpeed;
        bottomLeft.MaxSpeed = MaxSpeed;
    }

    public void Turn(float turnInput)
    {
        float angleLeft;
        float angleRight;
        if (turnInput > 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * turnInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * turnInput;
        }
        else if (turnInput < 0)
        {
            angleLeft  = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * turnInput;
            angleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * turnInput;
        }
        else
        {
            angleLeft  = 0;
            angleRight = 0;
        }

        topRight.Turn(angleRight);
        topLeft.Turn(angleLeft);
    }
    
    public void Accelerate(float speed)
    {
        topRight.Accelerate(speed);
        topLeft.Accelerate(speed);
        bottomLeft.Accelerate(speed);
        bottomRight.Accelerate(speed);
    }

    public bool IsGrounded()
    {
        if (topRight.IsGrounded()
                || topLeft.IsGrounded()
                || bottomLeft.IsGrounded()
                || bottomRight.IsGrounded())
        {
            return true;
        }
        return false;
    }
}
