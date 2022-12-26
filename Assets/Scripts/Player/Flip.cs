using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Flip : MonoBehaviour
{
    [SerializeField] private float unflipTime;
    private float flippedTime;
    private bool flipped;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            flipped = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            flipped = false;
            flippedTime = 0f;
        }
    }

    void Update()
    {
        if (flipped)
        {
            flippedTime += Time.deltaTime;
        }
    }

    public void Unflip()
    {
        if (flippedTime >= unflipTime)
        {
            transform.rotation = transform.rotation * Quaternion.AngleAxis(180, Vector3.forward);
        }
    }
}
