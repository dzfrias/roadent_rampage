using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CollisionSensitive : MonoBehaviour, IHittable
{
    [SerializeField] private float force;
    [SerializeField] private float upForce;

    private Rigidbody rb;
    private Collision collide;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (collide != null)
        {
            rb.AddExplosionForce(collide.relativeVelocity.magnitude * force, collide.GetContact(0).point, 10f, upForce);
            collide = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collide = collision;
        }
    }

    public void Hit(Vector3 hitPoint, float shootForce)
    {
        rb.AddExplosionForce(force * shootForce, hitPoint, 10f, upForce);
    }
}
