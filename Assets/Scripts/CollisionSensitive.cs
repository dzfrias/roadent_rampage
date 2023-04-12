using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CollisionSensitive : MonoBehaviour, IHittable
{
    [SerializeField] private float force;
    [SerializeField] private float upForce;
    [SerializeField] private float hitForce = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.contactCount != 0)
        {
            rb.AddExplosionForce(collision.relativeVelocity.magnitude * force, collision.GetContact(0).point, 10f, upForce);
        }
    }

    public void Hit(Vector3 hitPoint, Vector3 shootDir)
    {
        AudioManager.instance.Play("hit");
        rb.AddExplosionForce(force * hitForce, hitPoint + shootDir * 2f, 10f, upForce);
    }
}
