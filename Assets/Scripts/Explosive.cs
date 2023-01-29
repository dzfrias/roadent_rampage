using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider))]
public class Explosive : MonoBehaviour, IHittable
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float upwardsModifier= 2f;

    private CinemachineImpulseSource impulseSource;

    void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        Explode();
    }

    void Explode()
    {
        impulseSource.GenerateImpulse();
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
