using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float upwardsModifier= 2f;

    void OnCollisionEnter(Collision collision)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hits)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb == null) continue;
            Explode(rb);
        }
        Destroy(gameObject);
    }

    void Explode(Rigidbody target)
    {
        target.AddExplosionForce(explosionForce * target.mass, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
