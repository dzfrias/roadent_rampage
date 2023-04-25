using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HitMove : MonoBehaviour, IHittable
{
    [SerializeField] private float hitForce = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Hit(Vector3 hitPoint, Vector3 shootDir)
    {
        rb.AddForce(-shootDir * hitForce);
    }
}
