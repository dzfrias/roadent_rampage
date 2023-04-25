using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollideRelease : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private float minVelocity;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > minVelocity)
        {
            Vector3 point = collision.contacts[0].point;
            GameObject p = Instantiate(particles, point, Quaternion.identity);
        }
    }
}
