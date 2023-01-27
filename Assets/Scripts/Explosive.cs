using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Explosive : MonoBehaviour, IHittable
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float upwardsModifier= 2f;

    public void Hit(Vector3 hitPoint, float force)
    {
        Explode();
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hits)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb == null) continue;
            rb.AddExplosionForce(explosionForce * rb.mass, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
