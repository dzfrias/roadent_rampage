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
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(hit.collider.GetComponent<EnemyMovement>().Knockback(8));
            }
        }
        else
        {
            Debug.Log("Hit nothing");
        }
    }
}
