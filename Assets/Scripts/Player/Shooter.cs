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
            if (hit.collider.gameObject.TryGetComponent(out IHittable hittableObject))
            {
                hittableObject.Hit(hit.point, 10);
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
