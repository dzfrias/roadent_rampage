using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateAmount;

    private Rigidbody rb;

    // Inputs
    private float accelerationInput;
    private float rotationInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = transform.forward * speed * 10f;
        rb.AddForce(accelerationInput * move * Time.fixedDeltaTime, ForceMode.Acceleration);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 100 * rotationInput, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

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
