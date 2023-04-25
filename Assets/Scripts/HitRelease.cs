using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRelease : MonoBehaviour, IHittable
{
    [SerializeField] private GameObject particles;
    [SerializeField] private bool playSound = true;

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        if (playSound) AudioManager.instance.Play("hit");
        GameObject p = Instantiate(particles, hitPoint, Quaternion.LookRotation(direction));
        p.GetComponent<ParticleSystem>().Play();
    }
}
