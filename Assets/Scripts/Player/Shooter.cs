using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float fireCooldown;

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
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
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
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCooldown);
        canShoot = true;
    }
}
