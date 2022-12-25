using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyCollide : MonoBehaviour
{
    [SerializeField] private float upFactor = 1000f;
    [SerializeField] private float collideFactor = 1000f;

    private Rigidbody playerRb;
    private Vector3 collide;

    void Start()
    {
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (collide.magnitude > 0)
        {
            playerRb.AddForce((-collide * collideFactor) + (Vector3.up * playerRb.mass * upFactor));
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
