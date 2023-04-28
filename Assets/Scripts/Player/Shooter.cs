using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float fireCooldown;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private UnityEvent onShoot;

    private bool shooting;
    private bool canShoot = true;

    public void SetShooting(float value)
    {
        shooting = value == 1;
    }

    void Update()
    {
        if (shooting && canShoot) Shoot();
    }

    void Shoot()
    {
        if (GameManager.instance.isPaused) return;
        AudioManager.instance.Play("shoot");
        // Ignores IgnoreRaycast and IgnoreShoot layers
        LayerMask mask = ~0b10000100;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity, mask))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}!");
            Component[] hittables = hit.collider.gameObject.GetComponents(typeof(IHittable));
            foreach (IHittable hittable in hittables)
            {
                Vector3 dist = -(hit.point - transform.position);
                hittable.Hit(hit.point, dist.normalized);
            }
        }
        else
        {
            Debug.Log("Hit nothing");
        }
        Instantiate(particleEffect, transform);
        onShoot?.Invoke();
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCooldown);
        canShoot = true;
    }
}
