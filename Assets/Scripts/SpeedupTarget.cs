using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpeedupTarget : MonoBehaviour, IHittable
{
    [SerializeField] private float speedForce = 30f;
    [SerializeField] private float hitCooldown = 2f;
    [SerializeField] private GameObject particles;
    [SerializeField] private Color hitColor;

    private bool canBeHit = true;
    private Material material;
    private Color original;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        material = GetComponent<Renderer>().material;
        original = material.color;
    }

    public void Hit(Vector3 hitPoint, Vector3 direction)
    {
        if (!canBeHit) return;

        player.GetComponent<Rigidbody>().AddForce(player.transform.forward * speedForce, ForceMode.VelocityChange);
        StartCoroutine("HitCooldown");
        material.DOColor(hitColor, hitCooldown / 2);
        Instantiate(particles, transform.position, transform.rotation);
    }

    IEnumerator HitCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(hitCooldown);
        material.DOColor(original, hitCooldown / 2);
        canBeHit = true;
    }
}
