using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Explosive : MonoBehaviour
{
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float upwardsModifier= 2f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Player"))
        {
            Explode(collidedObject);
        }
    }

    private void Explode(GameObject target)
    {
        target.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.VelocityChange);
        Destroy(gameObject);
    }
}
