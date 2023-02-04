using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRelease : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject particles;

    void OnCollisionEnter(Collision collision)
    {
        GameObject p = Instantiate(particles, collision.contacts[0].point, Quaternion.identity);
        p.GetComponent<ParticleSystem>().Play();
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        AudioManager.instance.Play("hit");
        GameObject p = Instantiate(particles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
    }
}
