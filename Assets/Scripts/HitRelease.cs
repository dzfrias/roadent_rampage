using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRelease : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject particles;

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        AudioManager.instance.Play("hit");
        GameObject p = Instantiate(particles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
    }
}
