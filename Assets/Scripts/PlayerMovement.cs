using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float accelerationInput;
    private float rotationInput;

    void OnAccelerate(InputValue value)
    {
        accelerationInput = value.Get<float>();
        Debug.Log("Accelerating: " + accelerationInput);
    }

    void OnTurn(InputValue value)
    {
        rotationInput = value.Get<float>();
        Debug.Log("Rotating: " + rotationInput);
    }
}
