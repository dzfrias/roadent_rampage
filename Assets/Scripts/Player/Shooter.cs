using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public void Shoot()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            Debug.Log($"Hit: {hit.transform.gameObject}!");
            if (hit.collider.gameObject.TryGetComponent(out IHittable hittableObject))
            {
                hittableObject.Hit((hit.collider.transform.position - hit.point).normalized, 10);
            }
        }
        else
        {
            Debug.Log("Hit nothing");
        }
    }
}
