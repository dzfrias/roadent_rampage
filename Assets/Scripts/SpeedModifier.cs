using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : MonoBehaviour, IDriveSurface
{
    [SerializeField] private float modifyAmount;

    public void WheelHit(Wheel wheel)
    {
        wheel.ApplyForce(wheel.Velocity() * modifyAmount);
    }
}
