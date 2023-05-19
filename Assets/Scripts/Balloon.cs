using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Balloon : MonoBehaviour, IHittable
{
    [SerializeField] private float lifetime = 5f;

    [Header("Explosion")]
    [SerializeField] private GameObject particles;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private Vector3 explosionOffset;
    [SerializeField] private float force = 10f;
    [SerializeField] private float upForce = 3f;

    IEnumerator Start()
    {
        yield return Countdown();
    }

    public void Pop()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        Rigidbody player = Array
                                    .Find(hits, collider => collider.CompareTag("Player"))?
                                    .gameObject
                                    .GetComponent<Rigidbody>();
        player?.AddExplosionForce(force, transform.position, explosionRadius * 10f, upForce, ForceMode.Impulse);
        // player?.Stun(stunDuration);
        Instantiate(particles, transform.position + explosionOffset, transform.rotation);
        Destroy(gameObject);
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(lifetime);
        Pop();
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        Pop();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + explosionOffset, explosionRadius);
    }
}
