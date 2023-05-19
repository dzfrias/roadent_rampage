using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerVelocityCap : MonoBehaviour
{
    [SerializeField] private float cap;

    private Collider thisCollider;

    void Start()
    {
        thisCollider = GetComponent<Collider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, cap);
    }
}
