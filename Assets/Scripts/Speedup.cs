using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedup : MonoBehaviour
{
    [SerializeField] private Rigidbody target;
    [SerializeField] private float force = 10f;

    public void Activate()
    {
        target.AddForce(target.gameObject.transform.forward * force, ForceMode.VelocityChange);
    }
}
