using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupTarget : MonoBehaviour, IHittable
{
    [SerializeField] private float speedForce = 30f;
    [SerializeField] private float hitCooldown = 2f;

    private bool canBeHit = true;

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        if (!canBeHit) { return; }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward * speedForce, ForceMode.VelocityChange);
        StartCoroutine("HitCooldown");
    }

    IEnumerator HitCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canBeHit = true;
    }
}
