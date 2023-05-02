using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollideRelease : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject particlesAlt;
    [SerializeField] private float minVelocity;
    [SerializeField] private float velocityThreshold;

    void OnValidate()
    {
        velocityThreshold = Mathf.Max(minVelocity, velocityThreshold);
    }

    void OnCollisionEnter(Collision collision)
    {
        float vel = collision.relativeVelocity.magnitude;
        if (vel > minVelocity)
        {
            Vector3 point = collision.contacts[0].point;
            GameObject finalParticles = particles;
            if (vel >= velocityThreshold && particlesAlt != null)
            {
                finalParticles = particlesAlt;
            }
            Instantiate(finalParticles, point, Quaternion.identity);
        }
    }
}
