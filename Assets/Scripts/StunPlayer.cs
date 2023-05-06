using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPlayer : MonoBehaviour
{
    [SerializeField] private float seconds;
    [SerializeField] private GameObject particles;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            player.Stun(seconds);
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
