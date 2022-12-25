using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyCollide : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float upForce;

    private Rigidbody playerRb;
    private Collision collide;

    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (collide != null)
        {
            playerRb.AddExplosionForce(collide.relativeVelocity.magnitude * force * playerRb.mass, collide.GetContact(0).point, 10f, upForce);
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
}
