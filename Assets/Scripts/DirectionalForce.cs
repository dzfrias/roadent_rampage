using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionalForce : MonoBehaviour
{
    [SerializeField] private Transform direction;
    [SerializeField] private GameObject effectedObject;
    [SerializeField] private float forceAmount;

    private Rigidbody rb;

    private void Awake()
    {
        if (effectedObject == null)
        {
            effectedObject = this.gameObject;
        }
        rb = effectedObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction.forward * forceAmount * Time.deltaTime);
    }
}
