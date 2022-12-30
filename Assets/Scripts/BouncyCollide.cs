using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyCollide : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float upForce;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody collisionRb = collision.rigidbody;
        collisionRb.AddExplosionForce(collision.relativeVelocity.magnitude * force * collisionRb.mass, collision.GetContact(0).point, 10f, upForce);
    }
}
