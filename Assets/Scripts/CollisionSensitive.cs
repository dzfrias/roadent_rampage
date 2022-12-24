using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CollisionSensitive : MonoBehaviour
{
    [SerializeField] private float upFactor = 1000f;

    private Rigidbody rb;
    private Vector3 collide;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (collide.magnitude > 0)
        {
            rb.AddForce(Vector3.up * rb.mass * upFactor + collide);
            collide = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collide = collision.relativeVelocity;
        }
    }
}
