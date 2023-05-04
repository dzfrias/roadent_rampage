using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float fireCooldown;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private UnityEvent onShoot;

    [Header("Heat Up")]
    [SerializeField] private float chargeAdd;
    [SerializeField] private float chargeRemove;
    [SerializeField] private float chargeWaitTime;
    [SerializeField] private ShooterCharge output;

    private float charge;
    private bool waitingForCharge;
    private bool shooting;
    private bool canShoot = true;

    public void SetShooting(float value)
    {
        shooting = value == 1;
    }

    void Update()
    {
        if (shooting && canShoot && !waitingForCharge)
        {
            Shoot();
            return;
        }
        if ((!shooting && charge >= chargeRemove * Time.deltaTime) || waitingForCharge)
        {
            charge -= chargeRemove * Time.deltaTime;
            if (charge > 0 && charge < 1)
            {
                output.Overcharge(false);
                waitingForCharge = false;
            }
        }
        if (output != null)
        {
            output.SetCharge(charge);
        }
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
        charge += chargeAdd;
        charge = Mathf.Min(charge, 100f);
        output.Bounce();
        if (charge == 100f)
        {
            waitingForCharge = true;
            output.Overcharge(true);
        }
        else
        {
            StartCoroutine(Cooldown());
        }
        onShoot?.Invoke();
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireCooldown);
        canShoot = true;
    }
}
